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
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position.Set(pos.x, pos.y - 1, pos.z);
    }
}
