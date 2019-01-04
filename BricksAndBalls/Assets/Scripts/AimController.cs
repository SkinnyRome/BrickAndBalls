using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    private LevelManager _levelManager;
    private Vector3 _position;
    private float _botCanvasSize;
    private float _topCanvasSize;

	// Use this for initialization
	void Start () {
		
	}


    public void Init(LevelManager lm, float bCS, float tCS) {
        _levelManager = lm;
        _botCanvasSize = bCS;
        _topCanvasSize = tCS;
    }
    
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN



        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.y > (0 + _botCanvasSize) && Input.mousePosition.y < (Screen.height - _topCanvasSize))
                _position = Camera.main.ScreenPointToRay(Input.mousePosition).origin; ;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Input.mousePosition.y > (0 + _botCanvasSize) && Input.mousePosition.y < (Screen.height - _topCanvasSize))
            {
                _levelManager.Shoot(_position);
                gameObject.SetActive(false);
            }

        }


#endif
        

#if UNITY_ANDROID

        if (Input.touches.Length != 0)
        {
            var touch = Input.touches[0];


            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // Debug.Log("Mouse position: " + Input.mousePosition.y + " Bot Canvas size: " + _botCanvasSize +
                // " Screen Height: " + Screen.height);
                if (touch.position.y > (0 + _botCanvasSize) && touch.position.y < (Screen.height - _topCanvasSize))
                    _position = Camera.main.ScreenPointToRay(touch.position).origin;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touch.position.y > (0 + _botCanvasSize) && touch.position.y < (Screen.height - _topCanvasSize))
                {
                    _levelManager.Shoot(_position);
                    gameObject.SetActive(false);
                }
            }

        }


#endif

    }

}
