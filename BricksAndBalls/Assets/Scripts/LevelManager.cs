using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public DeadZone _deadZone;
    public BallSink _ballSink;
    public BallSpawner _ballSpawner;
    public MapGenerator _mapGenerator;
    public CanvasManager _canvasManager;
    public BoardManager _boardManager;
    public AimController _aimController;
    public SizeManager _sizeManager;

    private bool _firstBallDetected;
    private uint _points;
    public uint _ballsToSpawn;
    private uint _ballsArrived;

    private float _topCanvasSize;
    private float _botCanvasSize;

    // Use this for initialization
    void Start () {

        _firstBallDetected = false;
        _sizeManager.Init(this);
        _deadZone.Init(this);
        _mapGenerator.Init(this);
        _boardManager.Init(this, _mapGenerator.CreateLevel());
        _aimController.Init(this, _botCanvasSize, _topCanvasSize);
        _canvasManager.Init(this);
        _ballsArrived = 0;
        _points = 0;     

	}
	
	// Update is called once per frame
	void Update () {
		
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
            _ballSink.MoveTo((Vector2)b.transform.position);
            _ballSink.Show();
            callbackBall(b);
        }
        else
            b.GoTo(_ballSink.transform.position, callbackBall);
    }

    private void callbackBall(Ball b) {
        _ballsArrived++;
        _ballSink.ballArrived();

        if (_ballsArrived == _ballsToSpawn)
        {
            newThrow();
        }

        Destroy(b.gameObject);
        
    }

    private void newThrow() {
        _ballsArrived = 0;
        _ballSpawner.MoveTo((Vector2)_ballSink.transform.position);
        _boardManager.fall();
        _firstBallDetected = false;
        _aimController.Activate();

    }


    public void TileDestroyed(BasicTile t, uint i, uint j) {

        switch (t.gameObject.layer) {
            case 0:
                break;

        }

        _points += 10;

        Destroy(t.gameObject);  
       
    }

    public void Shoot(Vector3 position) {

        _ballSink.Hide();

        Vector3 dir = position - _ballSpawner.transform.position;

        float module = Mathf.Sqrt(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2));
        dir.x = dir.x / module;
        dir.y = dir.y / module;

        _ballSpawner.SpawnBalls(_ballsToSpawn, dir);

    }

    public void SetBotCanvasSize(float size)
    {
        _botCanvasSize = size;
    }

  
    public void SetTopCanvasSize(float size)
    {
        _topCanvasSize = size;
    }

  

}
