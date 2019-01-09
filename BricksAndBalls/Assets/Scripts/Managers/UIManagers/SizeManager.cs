using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This game object manages the size of the camera, canvas and gameplay map.
/// </summary>
public class SizeManager : MonoBehaviour {

    public GameObject  _botCanvas;
    public GameObject _topCanvas;
    public Canvas _parentCanvas;
    public Canvas _topCanvasC;
    public Canvas _botCanvasC;
    public Camera _mainCamera;

    private float tableroWidthUnidades = 11.5f;
    private float boardUnitHeight = 14.0f;

	// Use this for initialization
	public void Init (LevelManager l) {

        Canvas.ForceUpdateCanvases();

        //Obtain the total screen canvas size in pixels
        float parentCanvasHeight = _parentCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;       

        //Get the top Canvas size in pixels
        float topCanvasPixelHeight = _topCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;
        l.SetTopCanvasSize(topCanvasPixelHeight);

        //Get the bot Canvas size in pixels
        float botCanvasPixelHeight = _botCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;
        l.SetBotCanvasSize(botCanvasPixelHeight);

        //Calculate the size of the space leave for the board in pixels
        float boardPixelHeight = parentCanvasHeight - botCanvasPixelHeight - topCanvasPixelHeight;

        //Calculate pixels per unit
        float pixelPerUnitHeight = boardPixelHeight / boardUnitHeight;

        //Whit this, we obatin the size in units that the we want the Canvas to take
        float botCanvasUnitHeight = topCanvasPixelHeight / pixelPerUnitHeight;
        float topCanvasUnitHeight = botCanvasPixelHeight / pixelPerUnitHeight;

        //Now, we set the camera size regarding bot and top Canvases and set the position right in the middle
        _mainCamera.orthographicSize = ((boardUnitHeight + botCanvasUnitHeight + topCanvasUnitHeight)/ 2.0f );
        float yPosition = ((boardUnitHeight) / 2.0f + (botCanvasUnitHeight / 2.0f) - topCanvasUnitHeight/2.0f) ; 
        _mainCamera.transform.position = new Vector3(5.5f, yPosition, -10); 
    
        //At this point, the size of the camera is setted correctly to see all the board, but if the screen is taller than wider,
        //we need to adjust it, so we set the width of the camera to the board width. 
        float cameraWidthUnits = (_mainCamera.orthographicSize * 2) * _mainCamera.aspect;
        if(cameraWidthUnits < tableroWidthUnidades)
        {
            
            float newCameraHeightSize = tableroWidthUnidades / _mainCamera.aspect;
            _mainCamera.orthographicSize = (newCameraHeightSize / 2);
            
        }

        Destroy(gameObject);
	}
	

}
