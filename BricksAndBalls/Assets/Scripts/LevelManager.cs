using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public DeadZone _deadZone;
    public BallSink _ballSink;
    public BallSpawner _ballSpawner;
    public MapGenerator _mapGenerator;
    public BoardManager _boardManager;

    private bool prueba;
    private bool _firstBallDetected;
    private uint _points;
    public uint _ballsToSpawn;
    private uint _ballsArrived;
    


    // Use this for initialization
    void Start () {
        _firstBallDetected = false;
        _deadZone.Init(this);
        _mapGenerator.Init(this);
        _boardManager.Init(this, _mapGenerator.CreateLevel());
        _ballsArrived = 0;
        _points = 0;
        _ballSpawner.SpawnBalls(_ballsToSpawn, new Vector2(10, 10));

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
            _ballsArrived++;
        }
        else
            b.GoTo(_ballSink.transform.position, callbackBall);
    }

    private void callbackBall(Ball b) {
        _ballsArrived++;
        if (_ballsArrived == _ballsToSpawn) {
            newThrow();
        }
        Destroy(b);
        _ballSink.ballArrived();
    }

    private void newThrow() {
        _ballsArrived = 0;
        _ballSink.Hide();
        _ballSpawner.MoveTo((Vector2)_ballSink.transform.position);
        _boardManager.fall();
        _firstBallDetected = false;



    }


    public void TileDestroyed(BasicTile t, uint i, uint j) {

        Destroy(t.gameObject);        
    }






}
