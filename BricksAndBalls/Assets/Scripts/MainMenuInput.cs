using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInput : MonoBehaviour {

    private Vector3 _position;
    private Vector2 _lastPosition;
    private float _botCanvasSize;
    private float _topCanvasSize;
    public Canvas levelsCanvas;
    private float levelsCanvasYLimitTop;
    private float levelsCanvasYLimitBot;
    private float levelsCanvasYOriginal;
    public float _swipeVel;
    

    // Use this for initialization
    void Start()
    {

    }


    public void Init(MainMenuManager m, float bCS, float tCS)
    {
        _botCanvasSize = bCS;
        _topCanvasSize = tCS;


        levelsCanvasYLimitTop = levelsCanvas.gameObject.transform.position.y;
        levelsCanvasYLimitBot = levelsCanvasYLimitTop - levelsCanvas.gameObject.GetComponent<RectTransform>().rect.height;

        Debug.Log("Bot: " + levelsCanvasYLimitBot + " Top: " + levelsCanvasYLimitTop);
        
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN


        if (Input.mousePosition.y > (0 + _botCanvasSize) && Input.mousePosition.y < (Screen.height - _topCanvasSize))
        {
            if(Input.mouseScrollDelta.y > 0 || Input.GetAxis("Vertical") > 0)
            {
                SwipeUpLevelCanvas();
            }
            else if(Input.mouseScrollDelta.y < 0 || Input.GetAxis("Vertical") < 0)
            {
                SwipeDownLevelCanvas();
            }
        }


            /*
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.y > (0 + _botCanvasSize) && Input.mousePosition.y < (Screen.height - _topCanvasSize))
                    _position = Camera.main.ScreenPointToRay(Input.mousePosition).origin; ;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (Input.mousePosition.y > (0 + _botCanvasSize) && Input.mousePosition.y < (Screen.height - _topCanvasSize))
                {
                    gameObject.SetActive(false);
                }

            }
            */

#endif


#if UNITY_ANDROID

            if (Input.touches.Length != 0)
        {
            var touch = Input.touches[0];


            if (touch.phase == TouchPhase.Began)
            {
                // Debug.Log("Mouse position: " + Input.mousePosition.y + " Bot Canvas size: " + _botCanvasSize +
                // " Screen Height: " + Screen.height);
                if (touch.position.y > (0 + _botCanvasSize) && touch.position.y < (Screen.height - _topCanvasSize))
                {
                    _position = Camera.main.ScreenPointToRay(touch.position).origin;
                    _lastPosition = new Vector2(_position.x, _position.y);
                 
                }

                
            }
            else if (touch.phase == TouchPhase.Moved)
            {

                if (_position.y > _lastPosition.y)
                {
                    SwipeUpLevelCanvas();
                }
                else if (_position.y < _lastPosition.y)
                {
                    SwipeDownLevelCanvas();
                }

                _lastPosition = new Vector2(_position.x, _position.y);

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                
            }

        }


#endif

    }

    private void SwipeUpLevelCanvas()
    {
        levelsCanvas.gameObject.transform.Translate(0, _swipeVel, 0);
        if(levelsCanvas.gameObject.transform.position.y > levelsCanvasYLimitTop)
        {
            Vector3 p = levelsCanvas.gameObject.transform.position;
            p.y = levelsCanvasYLimitTop;
            levelsCanvas.gameObject.transform.position = p;
        }
    }

    private void SwipeDownLevelCanvas()
    {
        levelsCanvas.gameObject.transform.Translate(0, -_swipeVel, 0);
        if (levelsCanvas.gameObject.transform.position.y < levelsCanvasYLimitBot)
        {
            Vector3 p = levelsCanvas.gameObject.transform.position;
            p.y = levelsCanvasYLimitBot;
            levelsCanvas.gameObject.transform.position = p;
        }
    }

}


