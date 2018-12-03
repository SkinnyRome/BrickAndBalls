using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public int _life;

	// Use this for initialization
	void Start () {
		
	}
	
    public void Init(int life, Vector2 pos)
    {
        Vector3 p,pp2;
        p = gameObject.transform.position;
        gameObject.transform.localPosition = pos;
        pp2 = gameObject.transform.position;
        _life = life;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        _life--;
        

        if (_life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
