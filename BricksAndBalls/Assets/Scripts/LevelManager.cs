using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public DeadZone _deadZone;
    public BallSink _ballSink;
    public BallSpawner _ballSpawner;
    public MapGenerator _mapGenerator;

    private bool _firstBallDetected;
    private uint _points;

    // Use this for initialization
    void Start () {
        _firstBallDetected = false;
        _deadZone.Init(this);
        _mapGenerator.Init(this);
        _mapGenerator.CreateLevel();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BallEnteredDeadZone(Ball b) {
                
        b.Stop();

        if (!_firstBallDetected)
        {
            _firstBallDetected = true;
            _ballSink.MoveTo((Vector2)b.transform.position);
            _ballSink.ballArrived();
        }
        else
            b.GoTo(_ballSink.transform.position, callbackBall);
    }

    private void callbackBall(Ball b) {
        Destroy(b);
        _ballSink.ballArrived();
    }

    public void TileDestroyed(BasicTile t, uint i, uint j) {

        Destroy(t.gameObject);
    }






}
