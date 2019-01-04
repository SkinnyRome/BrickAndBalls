using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


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

    private bool _firstBallDetected;
    private bool _almostDead;
    private uint _starsScore;
    private uint _points;
    private uint _prize;
    private uint _ballsToSpawn;
    private uint _ballsArrived;
    private int _level;
    private bool _ballsThrowed;
    private bool _gamePaused;

    private float _topCanvasSize;
    private float _botCanvasSize;

    private float _timeSinceThrow;
    private float _throwTime;
    private const float WAITING_MAX_TIME = 5.0f;
    private const int MAX_ACCELERATIONS = 2;

    // Use this for initialization
    void Awake () {

        Init();
        _sizeManager.Init(this);
        _deadZone.Init(this);
        _mapGenerator.Init(this);
        _boardManager.Init(this, _mapGenerator.CreateLevel(GameManager.instance.GetSelectedLevelName()));
       // _boardManager.Init(this, _mapGenerator.CreateLevel("mapdata" + "4"));
        _aimController.Init(this, _botCanvasSize, _topCanvasSize);
        _ballManager.Init(this);
        _canvasManager.Init(this);
        _powerUpManager.Init(this);
	}

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
        LoadBallsNumber();
        UpdateUI();



    }

    public BoardManager GetBoardManager()
    {
        return _boardManager;
    }

    public void BallEnteredDeadZone(Ball b) {
                
        b.Stop();
        if (!_firstBallDetected)
        {
            _firstBallDetected = true;
            _ballSink.MoveTo(new Vector2(b.transform.position.x, b.transform.position.y + 0.2f));
            _ballSink.Show();
            CallbackBall(b);
        }
        else
            b.GoTo(_ballSink.transform.position, CallbackBall);
    }

    private void CallbackBall(Ball b) {
        _ballsArrived++;
        _ballSink.BallArrived();

        if (_ballsArrived == _ballsToSpawn)
        {
            ThrowEnded();
        }

        _ballManager.RemoveBall(b.gameObject);
        
    }

    private void ThrowEnded() {
        //TODO: Desactivar la imagen de precaucion
        _warningRow.SetActive(false);
        _ballsThrowed = false;

        //Level finished
        if (_boardManager.LevelCompleted())
        {
            Debug.Log("NIVEL TERMINADO");
            GameManager.instance.LevelFinished(_starsScore);
            _canvasManager.EndGameSuccess();
        }
        else
        {
            _boardManager.Fall();

            if (_boardManager.CheckFirstRow())
            {
                //GameOver
                Debug.Log("HAS MUERTO!!!!");
                _canvasManager.EndGameFailed();
                //GameManager.instance.GameOver();
            }
            else
            {
                //Care, the player is about to lose
                if (_boardManager.CheckWarningRow())
                {
                    
                    Debug.Log("casi voy a morir, CUIDADO!");
                    _warningRow.SetActive(true);
                }

                NewThrow();

            }
        }             
    }

    private void NewThrow() {
        _ballsArrived = 0;
        _ballManager.MoveTo((Vector2)_ballSink.transform.position);
        _prize = 10;
       
        _firstBallDetected = false;
        _aimController.Activate();

    }


    public void TileDestroyed(BasicTile t, uint i, uint j) {

        switch (t.gameObject.layer) {
            case 9://brick
                break;

        }

        _points += _prize;
        _prize += 10;
        UpdateUI();

        Destroy(t.gameObject);  
       
    }

    private void UpdateUI() {
        
        _starsScore = _points / 300; //Every 300 points 1 star, for example xD
        
        if (_starsScore >= 3)
            _starsScore = 3;

        _starsUI.text = "STARS: " + _starsScore.ToString();
        _scoreUI.text = _points.ToString();
    }

    public void Shoot(Vector3 position) {

        _ballSink.Hide();

        Vector3 dir = position - _ballManager.transform.position;

        float module = Mathf.Sqrt(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2));
        dir.x = dir.x / module;
        dir.y = dir.y / module;

        _throwTime = Time.time;
        _ballsThrowed = true;
        StartCoroutine(CheckElapsedTime());

        _ballManager.SpawnBalls(_ballsToSpawn, dir);

    }

    public void SetBotCanvasSize(float size)
    {
        _botCanvasSize = size;
    }

  
    public void SetTopCanvasSize(float size)
    {
        _topCanvasSize = size;
    }

    public void Pause()
    {
        _gamePaused = true;
        _ballManager.Pause();
        _canvasManager.Pause();
    }

    public void Resume()
    {
        _gamePaused = false;
        _ballManager.Resume();
        _canvasManager.Resume();
       
    }

    public void RetryLevel()
    {
        GameManager.instance.RetryLevel();
    }
    public void FreeRuby(GameObject g)
    {
        g.SetActive(false);
        GameManager.instance.DisplayRewardedAd();
    }

    public void GoHome()
    {
        GameManager.instance.GoMainMenu();
    }

    public void NextLevel()
    {
        GameManager.instance.LoadLevel((uint)(_level + 1));
    }

    private void LoadBallsNumber()
    {
        TextAsset mapsInfo =(TextAsset)Resources.Load("Maps/Mapsdata/gamedata_savelv");
        string levelData = mapsInfo.text.Split(new char[] { '\r' })[_level-1];
        string[] auxText = levelData.Split(',');
        auxText = auxText[1].Split(' ');
        _ballsToSpawn = uint.Parse(auxText[1]);

    }

    private IEnumerator CheckElapsedTime()
    {

        int accelerations = 0;

        while (_ballsThrowed)
        {
            yield return new WaitForSeconds(WAITING_MAX_TIME);
            if (_ballsThrowed && !_gamePaused)
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

    private void AccelerateGame()
    {
        _accelerateImage.GetComponent<FadeInTime>().Fade();
        _ballManager.AccelerateBalls();
        Debug.Log("Accelerate");
    }
}
