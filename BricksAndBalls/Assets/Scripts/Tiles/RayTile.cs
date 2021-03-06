﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum used to differenciate between Horizontal and Vertical ray.
/// </summary>
public enum RAY_TYPE {VERTICAL = 0, HORIZONTAL = 1 };

/// <summary>
/// Tile which, on hitted, do damage to all the other tile on its row or its column
/// </summary>
public class RayTile : BasicTile {

    public RAY_TYPE _rayType;
    private BoardManager _boardManager;
    private bool _hitted;
    private Transform _lightRenderer;

    /// <summary>
    /// Initialize the tile and its attributes
    /// </summary>
    /// <param name="pos">The position</param>
    /// <param name="father">The BoarManager GameObject</param>
    /// <param name="lm">The LevelManager GameObject</param>
    public void Init(Vector2 pos, GameObject father, LevelManager lm) {

        base.Init(1, pos, father, lm);

        _tileType = (_rayType == RAY_TYPE.HORIZONTAL) ? TILE_TYPE.HRAY : TILE_TYPE.VRAY;
        _boardManager = lm.GetBoardManager();
        _hitted = false;
        _needToBeDestroyed = false;

        _lightRenderer = transform.Find("Light");
        _lightRenderer.localScale = new Vector3(14.0f ,0 ,1.0f) ;
       
        
    }

    /// <summary>
    /// On hitted, call BoardManager to hit the correspondent tiles
    /// </summary>
    /// <param name="collision">Colision info</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        _hitted = true;

        if (_rayType == RAY_TYPE.HORIZONTAL)
        {
            _boardManager.HitRow(_row);
                    
        }
        else
        {
            _boardManager.HitColumn(_column);
        }

        StartCoroutine(LigthFade());
    }

    public override void Fall()
    {
        base.Fall();
        if(_hitted)
            _levelManager.TileDestroyed(this, _row, _column);

    }

    public override void DecreaseLife(int i)
    {
        //Do nothing
    }

    /// <summary>
    /// Coroutine which manages the Ray sprite by doing a fade with its Y scale to show the effect.
    /// </summary>
    private IEnumerator  LigthFade()
    {

        Vector3 scale = new Vector3(14.0f, 1.0f, 1.0f);
        _lightRenderer.localScale = scale;

        while (_lightRenderer.localScale.y > 0)
        {

            yield return new WaitForSeconds(0.02f);
            scale.y -= 0.1f; ;
            _lightRenderer.localScale = scale;

        }
        
    }
}
