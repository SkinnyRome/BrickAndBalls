using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Game object that manages the whole level, from the points to the progress, etc.
/// </summary>
public class LevelManager : MonoBehaviour
{

    //Game Objects
    public DeadZone _deadZone;
    public BallSink _ballSink;
    public BallManager _ballManager;
    public MapGenerator _mapGenerator;
    public PowerUpManager _powerUpManager;
    public BoardManager _boardManager;
    public AimController _aimController;
    public SizeManager _sizeManager;
    public CanvasManager _canvasManager;
    public GameObject _warningRow;
    public UnityEngine.UI.Text _starsUI;
    public UnityEngine.UI.Text _scoreUI;
    public GameObject _accelerateImage;

    //Flags
    private bool _firstBallDetected;
    private bool _almostDead;
    private uint _starsScore;
    private uint _points;
    private uint _prize;
    private uint _ballsToSpawn;
    private uint _ballReserve;
    private uint _additionalBallsThisThrow;
    private uint _ballsArrived;
    private uint _throwNumber;
    private uint _pointsPerStar;
    private int _level;
    private bool _ballsThrowed;
    private bool _gamePaused;

    //Canvas size
    private float _topCanvasSize;
    private float _botCanvasSize;
    
    //Numeric values
    private float _throwTime;
    private const float WAITING_MAX_TIME = 10.0f;
    private const int MAX_ACCELERATIONS = 2;
    private const uint MAX_BALLS_NUMBER = 100;

    /// <summary>
    /// Initialize the level manager and all the rest game objects that needs it
    /// </summary>
    void Awake()
    {

        Init();
        _sizeManager.Init(this);
        _deadZone.Init(this);
        _mapGenerator.Init(this);
        _boardManager.Init(this, _mapGenerator.CreateLevel(GameManager.instance.GetSelectedLevelName()));
        // _boardManager.Init(this, _mapGenerator.CreateLevel("mapdata" + "4"));
        _aimController.Init(this, _botCanvasSize, _topCanvasSize);
        _ballManager.Init(this, _ballsToSpawn);
        _canvasManager.Init(this);
        _powerUpManager.Init(this);
    }
    /// <summary>
    /// The private initializer of the level manager
    /// </summary>
    private void Init()
    {
        _level = (int)GameManager.instance.GetSelectedLevelNumber();
        _firstBallDetected = false;
        _ballsArrived = 0;
        _points = _starsScore = 0;
        _prize = 10;
        _warningRow.SetActive(false);
        _ballsThrowed = false;
        _gamePaused = false;
        _throwNumber = 0;
        _additionalBallsThisThrow = 0;
        _pointsPerStar = (uint)(1000 * ((_level / 10) + 1));
        LoadBallsNumber();
        UpdateStarsAndUI();

    }
    /// <summary>
    /// Getter of the board manager
    /// </summary>
    /// <returns>The board manager</returns>
    public BoardManager GetBoardManager()
    {
        return _boardManager;
    }
    /// <summary>
    /// Method that notifies that a ball has entered the dead zone
    /// </summary>
    /// <param name="b">The ball</param>
    public void BallEnteredDeadZone(Ball b)
    {

        b.Stop();
        if (!_firstBallDetected)
        {
            _firstBallDetected = true;
            _ballSink.MoveTo(new Vector2(b.transform.position.x, b.transform.position.y + 0.2f));
            _ballSink.Show();
            _ballManager.HideText();
            CallbackBall(b);
        }
        else
            b.GoTo(_ballSink.transform.position, CallbackBall);
    }
    /// <summary>
    /// The callback method of the ball that must be invoked by the ball.
    /// </summary>
    /// <param name="b"></param>
    private void CallbackBall(Ball b)
    {
        _ballsArrived++;
        _ballSink.BallArrived();

        uint totalBalls = _ballsToSpawn + _additionalBallsThisThrow;


        if (_ballsArrived == totalBalls)
        {
            ThrowEnded();
        }

        _ballManager.RemoveBall(b.gameObject);

    }
    /// <summary>
    /// Notify that the throw has ended and check if the level has been completed, its a gameover or continue
    /// </summary>
    private void ThrowEnded()
    {
        _warningRow.SetActive(false);
        _ballsThrowed = false;
        _canvasManager.OnThrowEnded();
        

        //Level finished
        if (_boardManager.LevelCompleted())
        {
            GameManager.instance.LevelFinished(_starsScore);
            _canvasManager.EndGameSuccess();
        }
        else
        {
            _boardManager.Fall();

            if (_boardManager.CheckFirstRow())
            {
                //GameOver
                _canvasManager.EndGameFailed();
                //GameManager.instance.GameOver();
            }
            else
            {
                //Care, the player is about to lose
                if (_boardManager.CheckWarningRow())
                {

                    _warningRow.SetActive(true);
                }

                NewThrow();

            }
        }
    }
    /// <summary>
    /// Prepare a new throw
    /// </summary>
    private void NewThrow()
    {
        _additionalBallsThisThrow = 0;
        _ballsArrived = 0;
        _ballManager.MoveTo((Vector2)_ballSink.transform.position);
        _ballManager.ShowText();
        _ballManager.ShowImage();
        _ballSink.Hide();
        _prize = 10;
        _throwNumber++;
        AddReserveBalls();

        _firstBallDetected = false;
        _aimController.Activate();

    }

    /// <summary>
    /// Notify when a tile needs to be destroyed
    /// </summary>
    /// <param name="t">Tile</param>
    /// <param name="i">I index</param>
    /// <param name="j">J index</param>
    public void TileDestroyed(BasicTile t, uint i, uint j)
    {

        switch (t.GetTileType())
        {
            case BasicTile.TILE_TYPE.BRICK:
                _points += _prize;
                _prize += 10;
                break;
            default:
                break;

        }

       
        UpdateStarsAndUI();

        Destroy(t.gameObject);

    }
    /// <summary>
    /// Update the stars and the points ui
    /// </summary>
    private void UpdateStarsAndUI()
    {

        _starsScore = _points / _pointsPerStar; 

        if (_starsScore >= 3)
            _starsScore = 3;

        _starsUI.text = "STARS: " + _starsScore.ToString();
        _scoreUI.text = _points.ToString();
    }
    /// <summary>
    /// Make a shoot of the game.
    /// </summary>
    /// <param name="position"></param>
    public void Shoot(Vector3 position)
    {
        Vector3 dir = position - _ballManager.transform.position;




        float module = Mathf.Sqrt(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2));
        dir.x = dir.x / module;
        dir.y = dir.y / module;

        if (dir.y > 0)
        {

            _ballSink.Hide();
            _throwTime = Time.time;
            _ballsThrowed = true;
            StartCoroutine(CheckElapsedTime());

            uint totalBalls = _ballsToSpawn + _additionalBallsThisThrow;

            _ballManager.SpawnBalls(totalBalls, dir);
            _ballManager.ShowImage();
            _ballManager.ShowText();
            _canvasManager.OnThrowStarted();
        }
        else
        {
            _aimController.Activate();
        }


    }
    /// <summary>
    /// Set the Bot canvas size
    /// </summary>
    /// <param name="size">Size</param>
    public void SetBotCanvasSize(float size)
    {
        _botCanvasSize = size;
    }

    /// <summary>
    /// Set the Top canvas size
    /// </summary>
    /// <param name="size">Size</param>
    public void SetTopCanvasSize(float size)
    {
        _topCanvasSize = size;
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause()
    {
        _gamePaused = true;
        _ballManager.Pause();
        _canvasManager.Pause();
    }

    /// <summary>
    /// Resume the game by resume the ball movement
    /// </summary>
    public void Resume()
    {
        _gamePaused = false;
        _ballManager.Resume();
        _canvasManager.Resume();

    }

    /// <summary>
    /// Retry this level
    /// </summary>
    public void RetryLevel()
    {
        GameManager.instance.RetryLevel();
    }
    /// <summary>
    /// Show a rewarded ad to the user
    /// </summary>
    /// <param name="g"></param>
    public void FreeRuby(GameObject g)
    {
        g.SetActive(false);
        GameManager.instance.DisplayRewardedAd();
    }

    /// <summary>
    /// Go main menu
    /// </summary>
    public void GoHome()
    {
        GameManager.instance.GoMainMenu();
    }

    /// <summary>
    /// Charge the next level
    /// </summary>
    public void NextLevel()
    {
        GameManager.instance.LoadLevel((uint)(_level + 1));
    }

    /// <summary>
    /// Load the initial available balls of this level at the start of the game.
    /// </summary>
    private void LoadBallsNumber()
    {
        TextAsset mapsInfo = (TextAsset)Resources.Load("Maps/Mapsdata/gamedata_savelv");
        
        string levelData = mapsInfo.text.Split(new char[] { '\r' })[_level - 1];
        string[] auxText = levelData.Split(',');
        auxText = auxText[1].Split(' ');
        _ballsToSpawn = uint.Parse(auxText[1]);

    }

    /// <summary>
    /// Coroutine to manage the time elapsed sinche the player throw the balls, to prevent the game for getting stuck
    /// by accelerating the balls and send them to the bottom.
    /// </summary>
    private IEnumerator CheckElapsedTime()
    {

        int accelerations = 0;
        uint myThrow = _throwNumber;

        while (_ballsThrowed)
        {
            yield return new WaitForSeconds(WAITING_MAX_TIME);
            if (_ballsThrowed && !_gamePaused && myThrow == _throwNumber)
            {
                if (accelerations < MAX_ACCELERATIONS)
                {
                    AccelerateGame();
                    accelerations++;
                }
                else
                {
                    _ballManager.DecreaseBallsDirections();
                }
            }

        }


    }

    /// <summary>
    /// Accelerate the balls and show the UI fade indicator.
    /// </summary>
    private void AccelerateGame()
    {
        _accelerateImage.GetComponent<FadeInTime>().Fade();
        _ballManager.AccelerateBalls();
    }

    /// <summary>
    /// Delete all the balls in the grid and terminate the throw.
    /// </summary>
    public void CollectBalls()
    {
        _ballManager.StopAndDeleteBalls();
        ThrowEnded();
    }

    /// <summary>
    /// Adds n balls to the _ballsReserve variable.
    /// </summary>
    /// <param name="n">Number of balls to add</param>
    public void AddBallsThisGame(uint n)
    {
        _ballReserve += n;

    }
    
    /// <summary>
    /// Add 'n' balls to _additionalBallsThisThrow variable.
    /// </summary>
    /// <param name="n">Number of additional balls</param>
    public void AddBallsThisShoot(uint n)
    {
        _additionalBallsThisThrow += n;
        _ballManager.SetBallNumber(_ballsToSpawn + _additionalBallsThisThrow);
    }
    /// <summary>
    /// Add the _ballRserver number of balls to the _ballsToSpawn variable.
    /// </summary>
    private void AddReserveBalls()
    {
        _ballsToSpawn += _ballReserve;
        if (_ballsToSpawn > MAX_BALLS_NUMBER)
            _ballsToSpawn = MAX_BALLS_NUMBER;
        _ballManager.SetBallNumber(_ballsToSpawn);

        _ballReserve = 0;
    }
}
