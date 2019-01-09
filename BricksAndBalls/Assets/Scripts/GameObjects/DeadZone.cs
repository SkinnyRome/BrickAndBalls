using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Game's dead zone
/// </summary>
public class DeadZone : MonoBehaviour {

 
    private LevelManager _levelManager;//Level Manager


    /// <summary>
    /// Initialize the object and assign the level manager
    /// </summary>
    /// <param name="lm">Level Manager</param>
    public void Init(LevelManager lm)
    {
        _levelManager = lm;
    }
    /// <summary>
    /// Notify when a collider entered the dead zone and if its a ball, notify the level manager
    /// </summary>
    /// <param name="c">Collider2D entered the dead Zone</param>
    public void OnTriggerEnter2D(Collider2D c)
    {

        
        Ball ball = c.gameObject.GetComponent<Ball>();
        if (ball != null)
            _levelManager.BallEnteredDeadZone(ball);

        

    }
}
