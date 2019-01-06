using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This tile adds 1, 2 or 3 balls to the game. The quantity of balls is setted in the Editor on the prefab.
/// </summary>
public class AditionalBallTile : BasicTile {

    private BoardManager _boardManager;
    private bool _hitted;
    private uint _ballsToAdd;

    public Sprite[] _sprites; //The sprites (1, 2 or 3)
  
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

    /// <summary>
    /// When the player hits this tile, this function call the levelManager to add the needed balls.
    /// </summary>
    /// <param name="collision">The collision info</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        _levelManager.AddBallsThisGame(_ballsToAdd);
        _levelManager.TileDestroyed(this, _row, _column);
        
    }

    public override void Fall()
    {
        base.Fall();
       

    }

    public override void DecreaseLife(int i)
    {
        //Do nothing
    }

    /// <summary>
    /// Set the correct sprite of the GameObject by selecting it in the array of sprite that have been selected in the editor.
    /// </summary>
    public void SetSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[_ballsToAdd - 1];
    }

    
}
