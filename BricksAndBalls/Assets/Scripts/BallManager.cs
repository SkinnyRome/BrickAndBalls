using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public GameObject _ball;
    private IEnumerator _throwBallCoroutine;
    public float _fUpdateTimes;
    public float _offsetY;
    private List<GameObject> _balls;
    private bool _paused;
    private LevelManager _levelManager;


	// Use this for initialization
	public void Init (LevelManager l) {
        _levelManager = l;
        _balls = new List<GameObject>();
        _paused = false;
		
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

        while(ballsSpawned < nBalls)
        {
            
                yield return new WaitForFixedUpdate();
                counter++;
                if (counter >= _fUpdateTimes && !_paused)
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
}
