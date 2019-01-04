using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public GameObject _ball;
    private IEnumerator _throwBallCoroutine;
    public float _fUpdateTimes;
    public float _offsetY;
    private List<GameObject> _balls;
    private int _ballsArrived;
    private bool _paused;
    private bool _stopSpawn;
    private LevelManager _levelManager;
    private TextMesh _text;
    private int _initialBalls;
    private SpriteRenderer _sprite;


	// Use this for initialization
	public void Init (LevelManager l, int iniBalls) {
        _levelManager = l;
        _text = gameObject.transform.Find("BallManagerText").gameObject.GetComponent<TextMesh>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _balls = new List<GameObject>();
        _paused = false;
        _stopSpawn = false;
        _initialBalls = iniBalls;

        _text.text = "x " + _initialBalls.ToString();
		
	}

    public void MoveTo(Vector2 position)
    {
        gameObject.transform.position = position;
    }



    public void SpawnBalls(uint n, Vector2 direction)
    {
        _throwBallCoroutine = ThrowBalls(n, direction);
        StartCoroutine(_throwBallCoroutine);
    }

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

        HideImage();
        
        _stopSpawn = false;

    }

    public void RemoveBall(GameObject b)
    {
        _balls.Remove(b);
        Destroy(b.gameObject);
    }

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

    public void AccelerateBalls()
    {
        foreach (GameObject b in _balls)
        {
            b.GetComponent<Ball>().Accelerate();
        }
    }

    public void DecreaseBallsDirections()
    {
        foreach (GameObject b in _balls)
        {
            b.GetComponent<Ball>().DecreaseDirection();
        }
    }

    public void StopAndDeleteBalls()
    {
        _stopSpawn = true;

        foreach (GameObject b in _balls)
        {
            Destroy(b);
        }
        _balls.Clear();
    }

    public void HideText()
    {
        _text.gameObject.SetActive(false);
    }
  
    public void HideImage()
    {
        Color hide = _sprite.color;
        hide.a = 0.0f;
        _sprite.color = hide; 
    }

    public void ShowText()
    {
        _text.gameObject.SetActive(true);
    }

    public void ShowImage()
    {
        Color show = _sprite.color;
        show.a = 1.0f;
        _sprite.color = show;
    }
}
