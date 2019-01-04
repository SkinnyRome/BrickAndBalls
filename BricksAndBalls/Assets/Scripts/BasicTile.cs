using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour {


    protected uint _row, _column;
    protected int _life;

    protected LevelManager _levelManager;
    protected bool _needToBeDestroyed;
    public enum TILE_TYPE { BRICK = 0, VRAY = 1, HRAY = 2 };

    protected TILE_TYPE _tileType;
	// Use this for initialization
	void Start () {
       
	}

    public virtual void Init(LevelManager lm)
    {
        
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
            _levelManager.TileDestroyed(this, (uint)gameObject.transform.localPosition.x, (uint)gameObject.transform.localPosition.y);

        //TODO: creo que _row y column no se actualizcan y por eso no las utilizo
    }
}
