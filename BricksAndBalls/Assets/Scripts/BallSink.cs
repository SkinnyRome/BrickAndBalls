using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSink : MonoBehaviour {

    public TextMesh label;
    private uint _numBalls;

	// Use this for initialization
	void Start () {
		
	}
    
    public void MoveTo(Vector2 position)
    {
        gameObject.transform.position = position;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);
    }
    public void ballArrived() {
        _numBalls++;
        label.text = "x" + _numBalls.ToString();           
    }


}
