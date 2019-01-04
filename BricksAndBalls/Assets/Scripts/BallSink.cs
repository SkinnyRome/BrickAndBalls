using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSink : MonoBehaviour {

    public TextMesh label;
    private uint _numBalls;
    public float YPosition; //Position at Y axis to always be above deadzone

	// Use this for initialization
	void Start () {
		
	}
    
    public void MoveTo(Vector2 position)
    {
        Vector2 p = new Vector2(position.x, YPosition);
        gameObject.transform.position = p;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        _numBalls = 0;
        gameObject.SetActive(true);
    }
    public void BallArrived() {
        _numBalls++;
        label.text = "x" + _numBalls.ToString();           
    }


}
