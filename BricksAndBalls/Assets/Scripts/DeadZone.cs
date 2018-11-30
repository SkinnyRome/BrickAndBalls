using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {


    public GameObject _ballSink;
    private bool _firstBallDetected;


	// Use this for initialization
	void Start () {
        _firstBallDetected = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 
    public void OnTriggerEnter2D(Collider2D c)
    {
        GameObject ball = c.gameObject;

        if (!_firstBallDetected)
        {
            _firstBallDetected = true;
            _ballSink.GetComponent<BallSink>().MoveTo((Vector2)ball.transform.position);
        }
        
        ball.GetComponent<Ball>().GoTo(_ballSink.transform.position);
        
    }
}
