using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script that gives the funcionability of a ball.
/// </summary>
public class Ball : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 _velocity = new Vector2(20, 20);
    private Vector2 _lastVelocity;
    public float _moveTime;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
     
    }

    /// <summary>
    /// Start moving the ball along the direction given
    /// </summary>
    /// <param name="direction">Direction</param>
    public void Shoot(Vector2 direction)
    {
        //TODO: si la velocidad es negativa, no lanzar ¿?
        
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.x *= _velocity.x;
        direction.y *= _velocity.y;

        rb.velocity = direction;
        _lastVelocity = rb.velocity;
        
       
    }
    /// <summary>
    /// Move the ball to a position and call the callback when arrived
    /// </summary>
    /// <param name="destPosition">Destination position</param>
    /// <param name="callback">Callback to be call when the ball arrives to destination</param>
    public void GoTo(Vector2 destPosition, System.Action<Ball> callback = null)
    {
        StartCoroutine(Move(destPosition, callback));
    }
    /// <summary>
    /// Stops the ball
    /// </summary>
    public void Stop() {
        rb.velocity = new Vector2(0, 0);
    }
    /// <summary>
    /// Private method that moves the ball by a coroutine
    /// </summary>
    /// <param name="dest">Destination</param>
    /// <param name="callback">Callback to be call when the ball arrives </param>
    /// <returns></returns>
    private IEnumerator Move(Vector2 dest, System.Action<Ball> callback = null)
    {

        float distance = dest.x - transform.position.x;        
        float step = _moveTime / distance;
        if (distance != 0)
        {

            int totalSteps = Mathf.Abs(Mathf.RoundToInt(distance / step));  

            for (int i = 0; i < totalSteps; i++)
            {
                yield return new WaitForSecondsRealtime(0.01f);
                gameObject.transform.Translate(new Vector3(step, 0, 0));
            }
        }

        gameObject.transform.transform.position = new Vector3(dest.x, dest.y);
        
        if (callback != null) {
            callback(this);
        }

    }

    /// <summary>
    /// Pause the ball's movement and store its velocity to later resume it.
    /// </summary>
    public void Pause()
    {
        _lastVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Resume the ball's movement whit it's last velocity.
    /// </summary>
    public void Resume()
    {
        rb.velocity = _lastVelocity;
        _lastVelocity = Vector2.zero;
    }

    /// <summary>
    /// Accelerate the ball velocity by multiplicate it by 2.
    /// </summary>
    public void Accelerate()
    {
        Vector2 newVelocity = rb.velocity;

        newVelocity.x *= 2;
        newVelocity.y *= 2;

        rb.velocity = newVelocity;
    }

    /// <summary>
    /// Decrease the ball direction.
    /// </summary>
    public void DecreaseDirection()
    {
        Vector2 newDirection = rb.velocity;

        newDirection.y -= 1;
  
        rb.velocity = newDirection;
    }
}
