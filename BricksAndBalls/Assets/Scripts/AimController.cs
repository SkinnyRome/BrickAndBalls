using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    private LevelManager _levelManager;
    private Vector3 _position;
    private float _botCanvasSize;
    private float _topCanvasSize;
    public GameObject _pointer;

	// Use this for initialization
	void Start () {
		
	}


    public void Init(LevelManager lm, float bCS, float tCS) {
        _levelManager = lm;
        _botCanvasSize = bCS;
        _topCanvasSize = tCS;
        _pointer.SetActive(false);
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
            {
                _position = Camera.main.ScreenPointToRay(Input.mousePosition).origin; ;
                _pointer.SetActive(true);
                _pointer.transform.position = new Vector3(Input.mousePosition.x, (Input.mousePosition.y - _pointer.GetComponent<RectTransform>().rect.height / 2), _pointer.transform.position.z);

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Input.mousePosition.y > (0 + _botCanvasSize) && Input.mousePosition.y < (Screen.height - _topCanvasSize))
            {
                _levelManager.Shoot(_position);
                _pointer.SetActive(false);
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
                {
                    _position = Camera.main.ScreenPointToRay(touch.position).origin;
                    _pointer.SetActive(true);
                    _pointer.transform.position.Set(_position.x, _position.y, _pointer.transform.position.z);

                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touch.position.y > (0 + _botCanvasSize) && touch.position.y < (Screen.height - _topCanvasSize))
                {
                    _levelManager.Shoot(_position);
                    _pointer.SetActive(false);
                    gameObject.SetActive(false);
                }
            }

        }


#endif

    }

}
