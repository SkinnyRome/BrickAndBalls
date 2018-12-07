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
}
