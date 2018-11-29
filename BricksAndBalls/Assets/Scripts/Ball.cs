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

    public void GoTo(Vector2 destPosition)
    {
        rb.velocity = new Vector2(0, 0);
        
        StartCoroutine(Move(destPosition));
    }

    private IEnumerator Move(Vector2 dest)
    {

        float distance = dest.x - transform.position.x;
        //dest = GetAbsoluteValue(dest);
        float step = dest.x / _moveTime;
        int totalSteps = Mathf.Abs(Mathf.RoundToInt(distance / step));
        float stepTime = totalSteps / _moveTime;

        Debug.Log(step + " " + stepTime + " " + totalSteps);
        for(int i = 0; i < totalSteps; i++)
        {
            yield return new WaitForSecondsRealtime(stepTime);
            gameObject.transform.Translate(new Vector3(step,0,0)); 
        }

        gameObject.transform.transform.position = new Vector3(dest.x, dest.y);
    }

    private Vector2 GetAbsoluteValue(Vector2 v)
    {
        return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }
}
