using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

 
    private LevelManager _levelManager;


	// Use this for initialization
	void Start () {
       
       
	}


    public void Init(LevelManager lm)
    {
        _levelManager = lm;
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        Ball ball = c.gameObject.GetComponent<Ball>();
        if (ball != null)
            _levelManager.BallEnteredDeadZone(ball);

        

    }
}
