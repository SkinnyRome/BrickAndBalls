using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSink : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    
    public void MoveTo(Vector2 position)
    {
        gameObject.transform.position = position;
    }
}
