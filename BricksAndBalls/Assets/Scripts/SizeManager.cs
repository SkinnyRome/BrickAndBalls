using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeManager : MonoBehaviour {

    public GameObject  _botCanvas;
    public GameObject _topCanvas;
    public Canvas _topCanvasC;
    public Canvas _botCanvasC;
    public Camera _mainCamera;


	// Use this for initialization
	void Start () {

        Canvas.ForceUpdateCanvases();

        float topCanvasSize = _topCanvas.GetComponent<RectTransform>().rect.height;
        
        float topSize = topCanvasSize * _topCanvasC.transform.localScale.y;

        float botCanvasSize = _botCanvas.GetComponent<RectTransform>().rect.height;

        float botSize = botCanvasSize * _botCanvasC.transform.localScale.y;

        float tableroHeight = (Screen.height - (topSize + botSize));

         Debug.Log("Tablero size :" + tableroHeight);


        float cameraSize = (_mainCamera.orthographicSize * 2);
        float cameraSizePixel = _mainCamera.pixelHeight;

        Debug.Log("Altura camara en unidades: " + cameraSize + " Altura en pixeles: " + cameraSizePixel);

        float ppuHeight = cameraSizePixel / cameraSize;

        Debug.Log("Pixeles por unidad: " + ppuHeight);

        _mainCamera.orthographicSize = ((tableroHeight / ppuHeight) / 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
