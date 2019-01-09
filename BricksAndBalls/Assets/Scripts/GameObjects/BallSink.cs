using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GameObject that collect the balls after colliding with the dead zone.
/// </summary>
public class BallSink : MonoBehaviour {

    public TextMesh label; //Label to indicate the number of balls arrived
    private uint _numBalls;//Number of balls arrived
    public float YPosition; //Position at Y axis to always be above deadzone

    /// <summary>
    /// Moves the ballSink into a given position
    /// </summary>
    /// <param name="position">Destination</param>
    public void MoveTo(Vector2 position)
    {
        Vector2 p = new Vector2(position.x, YPosition);
        gameObject.transform.position = p;
    }
    /// <summary>
    /// Hide the ballSink
    /// </summary>
    public void Hide() {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Show the ballSink
    /// </summary>
    public void Show() {
        _numBalls = 0;
        gameObject.SetActive(true);
    }
    /// <summary>
    /// Method that notifies that a ball has arrived the ballSink
    /// </summary>
    public void BallArrived() {
        _numBalls++;
        label.text = "x" + _numBalls.ToString();           
    }


}
