using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    public GameObject _ball;
    //Provisional
    public uint _numBalls;

    private IEnumerator _throwBallCoroutine;
    public float _fUpdateTimes;
    public float _offsetY; 

	// Use this for initialization
	void Start () {

        SpawnBalls(_numBalls, new Vector2(10, 10));
		
	}

    void SpawnBalls(uint n, Vector2 direction)
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
            if (counter >= _fUpdateTimes) {
                Vector3 position = new Vector3(transform.position.x, transform.position.y + _offsetY, 0);
                GameObject b = Object.Instantiate(_ball, position, Quaternion.identity);
                b.GetComponent<Ball>().Shoot(direction);
                counter = 0;
                ballsSpawned++;
            }            
        }

    }
}
