﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RAY_TYPE {VERTICAL = 0, HORIZONTAL = 1 };

public class RayTile : BasicTile {

    public RAY_TYPE _rayType;
    private BoardManager _boardManager;
    private bool _hitted;
    private SpriteRenderer _lightRenderer;
    // Use this for initialization
    void Start () {
		
	}

    public void Init(Vector2 pos, GameObject father, LevelManager lm) {

        _tileType = (_rayType == RAY_TYPE.HORIZONTAL) ? TILE_TYPE.HRAY : TILE_TYPE.VRAY;
        _levelManager = lm;
        _row = (uint)pos.x;
        _column = (uint)pos.y;
        transform.parent = father.transform;
        gameObject.transform.localPosition = pos;
        _life = 1;
        _boardManager = lm.GetBoardManager();
        _hitted = false;
        _needToBeDestroyed = false;

        //_lightRenderer = transform.Find("Light").gameObject.GetComponent<SpriteRenderer>();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        _hitted = true;

        if (_rayType == RAY_TYPE.HORIZONTAL)
        {
            _boardManager.HitRow((int)gameObject.transform.localPosition.y);
            //LigthFade();           
        }
        else
        {
            _boardManager.HitColumn((int)gameObject.transform.localPosition.x);
        }


    }

    public override void Fall()
    {
        base.Fall();
        if(_hitted)
            _levelManager.TileDestroyed(this, _row, _column);

    }

    public override void DecreaseLife(uint i)
    {
        
    }

    private IEnumerator  LigthFade()
    {
        
        
        while (_lightRenderer.color.a > 0)
        {

            yield return new WaitForSeconds(0.1f);
            Color c = _lightRenderer.color;
            c.a -= 0.1f;
            _lightRenderer.color = c;

        }
        
    }
}
