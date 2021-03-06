﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The father class of all tiles. It have the common functionality.
/// </summary>
public class BasicTile : MonoBehaviour {


    protected uint _row, _column;
    protected int _life;

    protected LevelManager _levelManager;
    protected bool _needToBeDestroyed;
    public enum TILE_TYPE { BRICK = 0, VRAY = 1, HRAY = 2, ADITIONAL_BALL = 3 };

    protected TILE_TYPE _tileType;

    public virtual void Init(int life, Vector2 pos, GameObject father, LevelManager lm)
    {
        _levelManager = lm;
        _row = (uint)pos.y;
        _column = (uint)pos.x;
        transform.SetParent(father.transform, false);
        gameObject.transform.localPosition = pos;
        _life = (int)life;
    }

    public TILE_TYPE GetTileType()
    {
        return _tileType;
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public uint GetRow() {
        return _row;
    }

    public bool NeedToBeDestroyed() {
        return _needToBeDestroyed;
    }

    public virtual void Fall() {
        Vector3 pos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(pos.x, pos.y - 1, pos.z);
        _row--;
    }

    public virtual void DecreaseLife(int i)
    {
        _life -= i;
        if (_life <= 0)
            Destroy();
    }

    public void Destroy()
    {
        _levelManager.TileDestroyed(this, (uint)gameObject.transform.localPosition.x, (uint)gameObject.transform.localPosition.y);

    }
}
