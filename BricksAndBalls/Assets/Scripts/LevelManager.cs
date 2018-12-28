using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public DeadZone _deadZone;
    public BallSink _ballSink;
    public BallManager _ballManager;
    public MapGenerator _mapGenerator;
    public PowerUpManager _canvasManager;
    public BoardManager _boardManager;
    public AimController _aimController;
    public SizeManager _sizeManager;

    private bool _firstBallDetected;
    private bool _almostDead;
    private uint _points;
    public uint _ballsToSpawn;
    private uint _ballsArrived;

    private float _topCanvasSize;
    private float _botCanvasSize;

    // Use this for initialization
    void Awake () {

        _firstBallDetected = false;
        _sizeManager.Init(this);
        _deadZone.Init(this);
        _mapGenerator.Init(this);
        //_boardManager.Init(this, _mapGenerator.CreateLevel(GameManager.instance.GetLevelNameSelected()));
        _boardManager.Init(this, _mapGenerator.CreateLevel("mapdata1"));
        _aimController.Init(this, _botCanvasSize, _topCanvasSize);
        _ballManager.Init(this);
        _canvasManager.Init(this);
        _ballsArrived = 0;
        _points = 0;     

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
            _ballSink.MoveTo(new Vector2(b.transform.position.x, b.transform.position.y - 0.5f));
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
        //Level finished
        if (_boardManager.LevelCompleted())
        {
            Debug.Log("NIVEL TERMINADO");
            GameManager.instance.LevelFinished();
        }
        else {
            if (_boardManager.CheckFirstRow())
            {
                //GameOver
                Debug.Log("HAS MUERTO!!!!");
                GameManager.instance.GameOver();
            }
            else {

                _boardManager.Fall();

                //Care, the player is about to lose
                if (_boardManager.CheckFirstRow())
                {
                    //TODO: Activar la imagen esa roja de precaucion
                    Debug.Log("casi voy a morir, CUIDADO!");
                }

                NewThrow();

            }
        }               
    }

    private void NewThrow() {
        _ballsArrived = 0;
        _ballManager.MoveTo((Vector2)_ballSink.transform.position);
       
        _firstBallDetected = false;
        _aimController.Activate();

    }


    public void TileDestroyed(BasicTile t, uint i, uint j) {

        switch (t.gameObject.layer) {
            case 9://brick
                break;

        }

        _points += 10;

        Destroy(t.gameObject);  
       
    }

    public void Shoot(Vector3 position) {

        _ballSink.Hide();

        Vector3 dir = position - _ballManager.transform.position;

        float module = Mathf.Sqrt(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2));
        dir.x = dir.x / module;
        dir.y = dir.y / module;

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
        _ballManager.Pause();
    }

}
