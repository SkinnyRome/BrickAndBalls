using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GameObject who manages the input at the GamePlay scene.
/// </summary>
public class AimController : MonoBehaviour {

    private LevelManager _levelManager;
    private Vector3 _position;
    private float _botCanvasSize;
    private float _topCanvasSize;
    public GameObject _pointer;

    /// <summary>
    /// Initialize the GameObjects
    /// </summary>
    /// <param name="lm">The LevelManager GameObject</param>
    /// <param name="bCS">The bottom canvas height in pixels</param>
    /// <param name="tCS">The top canvas height in pixels</param>
    public void Init(LevelManager lm, float bCS, float tCS) {
        _levelManager = lm;
        _botCanvasSize = bCS;
        _topCanvasSize = tCS;
        _pointer.SetActive(false);
    }
    
    /// <summary>
    /// Activates the GameObject to star manage the input
    /// </summary>
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// In the Update method, controls the player input to take the direction which the balls must take.
    /// </summary>
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
                gameObject.SetActive(false);
                _levelManager.Shoot(_position);
            }
                _pointer.SetActive(false);

        }


#endif
        

#if UNITY_ANDROID

        if (Input.touches.Length != 0)
        {
            var touch = Input.touches[0];


            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
               
                if (touch.position.y > (0 + _botCanvasSize) && touch.position.y < (Screen.height - _topCanvasSize))
                {
                    _position = Camera.main.ScreenPointToRay(touch.position).origin;
                    _pointer.SetActive(true);
                    _pointer.transform.position = new Vector3(touch.position.x, touch.position.y, _pointer.transform.position.z);

                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touch.position.y > (0 + _botCanvasSize) && touch.position.y < (Screen.height - _topCanvasSize))
                {
                    gameObject.SetActive(false);
                    _levelManager.Shoot(_position);
                }
                    _pointer.SetActive(false);
            }

        }


#endif

    }

}
