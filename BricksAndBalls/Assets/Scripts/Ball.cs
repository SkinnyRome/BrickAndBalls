using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb;

    public float _moveTime;

    //private float t = 0.0f;
    //private bool moving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
     
    }


    public void Shoot(Vector2 direction)
    {
        //TODO: si la velocidad es negativa, no lanzar ¿?
        rb.velocity = direction;
       
    }

    public void GoTo(Vector2 destPosition, System.Action<Ball> callback = null)
    {
        StartCoroutine(Move(destPosition, callback));
    }

    public void Stop() {
        rb.velocity = new Vector2(0, 0);
    }

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

    private Vector2 GetAbsoluteValue(Vector2 v)
    {
        return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }
}
