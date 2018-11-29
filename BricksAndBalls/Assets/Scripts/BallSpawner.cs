using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public GameObject _ball;
    public int _numBalls;

    private IEnumerator _throwBallCoroutine;
    public float _seconds;
    public float _offsetY; 

	// Use this for initialization
	void Start () {

        SpawnBalls(_numBalls, new Vector2(10, 10));
		
	}

    void SpawnBalls(int n, Vector2 direction)
    {
        _throwBallCoroutine = ThrowBalls(direction);
        StartCoroutine(_throwBallCoroutine);
    }

    private IEnumerator ThrowBalls(Vector2 direction)
    {
        int ballsSpawned;
        for (ballsSpawned = 0; ballsSpawned < _numBalls; ballsSpawned++)
        {
            yield return new WaitForSecondsRealtime(_seconds);
            Vector3 position = new Vector3(transform.position.x, transform.position.y + _offsetY, 0);
            GameObject b = Object.Instantiate(_ball, position, Quaternion.identity);
            b.GetComponent<Ball>().Shoot(direction);
        }

    }
}
