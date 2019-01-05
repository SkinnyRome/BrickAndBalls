using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AditionalBallTile : BasicTile {

    private BoardManager _boardManager;
    private bool _hitted;
    private uint _ballsToAdd;

    public Sprite[] _sprites;
    // Use this for initialization
    void Start()
    {

    }

    public void Init(Vector2 pos, GameObject father, LevelManager lm, uint nBalls)
    {

        base.Init(1, pos, father, lm);
        _boardManager = lm.GetBoardManager();
        _hitted = false;
        _needToBeDestroyed = false;
        _ballsToAdd = nBalls;
        _tileType = TILE_TYPE.ADITIONAL_BALL;

        SetSprite();

 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        _levelManager.AddBallsThisMatch(_ballsToAdd);
        _levelManager.TileDestroyed(this, _row, _column);


        //StartCoroutine(LigthFade());
    }

    public override void Fall()
    {
        base.Fall();
       

    }

    public override void DecreaseLife(int i)
    {
        //Do nothing
    }

    public void SetSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[_ballsToAdd - 1];
    }

    
}
