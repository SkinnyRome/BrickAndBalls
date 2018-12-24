using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour {


    protected uint _life, _row, _column;

    protected LevelManager _levelManager;
 
	// Use this for initialization
	void Start () {
       
	}

    public virtual void Init(LevelManager lm)
    {
        
    }


    public virtual void fall() {
        Vector3 pos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(pos.x, pos.y - 1, pos.z);
    }

    public virtual void DecreaseLife(uint i)
    {
        _life -= i;
        if (_life <= 0)
            _levelManager.TileDestroyed(this, (uint)gameObject.transform.localPosition.x, (uint)gameObject.transform.localPosition.y);

        //TODO: creo que _row y column no se actualizcan y por eso no las utilizo
    }
}
