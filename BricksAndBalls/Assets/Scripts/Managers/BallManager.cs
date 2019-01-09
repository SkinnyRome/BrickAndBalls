using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Game object that manages the balls.
/// </summary>
public class BallManager : MonoBehaviour {

    public GameObject _ball;//Prefab to be spawned.
    private IEnumerator _throwBallCoroutine;
    public float _fUpdateTimes;//Counter to the spawner
    public float _offsetY;//To be sure that the balls are always above dead zone
    private List<GameObject> _balls;//List to manage all the balls    
    private bool _paused;
    private bool _stopSpawn;
    private LevelManager _levelManager;//Level manager
    private TextMesh _text;//Label to indicate the balls to spawn
    private uint _ballsNumber;//Number of balls to spawn
    private SpriteRenderer _sprite;//Own sprite


	
    /// <summary>
    /// Initialize the object and assign the level manager and the number of balls to spawn at first 
    /// </summary>
    /// <param name="l">Level Manager</param>
    /// <param name="iniBalls">Number of balls to spawn</param>
	public void Init (LevelManager l, uint iniBalls) {

        _levelManager = l;
        _text = gameObject.transform.Find("BallManagerText").gameObject.GetComponent<TextMesh>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _balls = new List<GameObject>();
        _paused = false;
        _stopSpawn = false;
        _ballsNumber = iniBalls;
        _text.text = "x " + _ballsNumber.ToString();
		
	}
    /// <summary>
    /// Move to a desired position
    /// </summary>
    /// <param name="position">Destination</param>
    public void MoveTo(Vector2 position)
    {
        gameObject.transform.position = position;
    }


    /// <summary>
    /// Spawn n number of balls into the direction given
    /// </summary>
    /// <param name="n">Number of balls to spawn</param>
    /// <param name="direction">The direction desired</param>
    public void SpawnBalls(uint n, Vector2 direction)
    {
        _stopSpawn = false;
        _ballsNumber = n;
        _throwBallCoroutine = ThrowBalls(n, direction);
        StartCoroutine(_throwBallCoroutine);
    }
    /// <summary>
    /// The spawn balls coroutine
    /// </summary>
    /// <param name="nBalls">Number of balls to spawn</param>
    /// <param name="direction">The direction desired</param>
    /// <returns></returns>
    private IEnumerator ThrowBalls(uint nBalls,  Vector2 direction)
    {
       
        int counter = 0;
        int ballsSpawned = 0;

        while(ballsSpawned < nBalls && !_stopSpawn)
        {
            
                yield return new WaitForFixedUpdate();
                counter++;
                if (counter >= _fUpdateTimes && !_paused && !_stopSpawn)
                {
                    Vector3 position = new Vector3(transform.position.x, transform.position.y + _offsetY, 0);
                    GameObject b = Object.Instantiate(_ball, position, Quaternion.identity);
                    Ball ball = b.GetComponent<Ball>();
                    ball.Shoot(direction);
                    _balls.Add(b);
                    counter = 0;
                    ballsSpawned++;
                }
            
        }
        if(!_stopSpawn)
            HideImage();
        
        _stopSpawn = false;

    }
    /// <summary>
    /// Remove a ball from the list and from the game
    /// </summary>
    /// <param name="b">The ball to be removed</param>
    public void RemoveBall(GameObject b)
    {
        _balls.Remove(b);
        Destroy(b.gameObject);
    }
    /// <summary>
    /// Pause all the balls of the list
    /// </summary>
    public void Pause()
    {
        if (!_paused)
        {
            _paused = true;
            foreach (GameObject b in _balls)
            {
                b.GetComponent<Ball>().Pause();
            }
        }
    }
    /// <summary>
    /// Resume all the balls from the list
    /// </summary>
    public void Resume()
    {
        if (_paused)
        {
            foreach (GameObject b in _balls)
            {
                b.GetComponent<Ball>().Resume();
            }

            _paused = false;
        }
    }
    /// <summary>
    /// Accelerate all the balls from the list
    /// </summary>
    public void AccelerateBalls()
    {
        foreach (GameObject b in _balls)
        {
            b.GetComponent<Ball>().Accelerate();
        }
    }
    /// <summary>
    /// Decrease the direction from the balls of the list
    /// </summary>
    public void DecreaseBallsDirections()
    {
        foreach (GameObject b in _balls)
        {
            b.GetComponent<Ball>().DecreaseDirection();
        }
    }
    /// <summary>
    /// Stop the spawner and delete all the balls from the list
    /// </summary>
    public void StopAndDeleteBalls()
    {
        _stopSpawn = true;

        foreach (GameObject b in _balls)
        {
            Destroy(b);
        }

        
        _balls.Clear();

    }
    /// <summary>
    /// Hide text
    /// </summary>
    public void HideText()
    {
        _text.gameObject.SetActive(false);
    }
    /// <summary>
    /// Hide the image
    /// </summary>
    public void HideImage()
    {
        Color hide = _sprite.color;
        hide.a = 0.0f;
        _sprite.color = hide; 
    }
    /// <summary>
    /// Show the text
    /// </summary>
    public void ShowText()
    {
        _text.text = "x " + _ballsNumber.ToString();
        _text.gameObject.SetActive(true);
    }
    /// <summary>
    /// Show the image
    /// </summary>
    public void ShowImage()
    {
        Color show = _sprite.color;
        show.a = 1.0f;
        _sprite.color = show;
    }
    /// <summary>
    /// Set the number of balls to be spawned
    /// </summary>
    /// <param name="b">Number of balls to spawn</param>
    public void SetBallNumber(uint b)
    {
        _ballsNumber = b;
        _text.text = "x " + _ballsNumber.ToString();

    }

}
