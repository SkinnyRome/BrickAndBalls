using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BasicTile {

    

	// Use this for initialization
	void Start () {
		
	}
	
    public void Init(uint life, Vector2 pos, GameObject father, LevelManager lm)
    {
        
        _levelManager = lm;
        _row = (uint)pos.x;
        _column = (uint)pos.y;
        transform.parent = father.transform;        
        gameObject.transform.localPosition = pos;        
        _life = life;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        _life--;        

        if (_life <= 0)
        {
            _levelManager.TileDestroyed(this, _row, _column);
        }
    }
}
