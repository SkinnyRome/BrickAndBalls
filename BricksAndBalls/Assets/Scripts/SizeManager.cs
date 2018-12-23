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

    private float tableroWidthUnidades = 11.25f;
    private float tableroHeightUnidades = 14;

	// Use this for initialization
	void Start () {

        Canvas.ForceUpdateCanvases();

        float topCanvasSize = _topCanvas.GetComponent<RectTransform>().rect.height;
        
        float topSize = topCanvasSize * _topCanvasC.transform.localScale.y;

        float botCanvasSize = _botCanvas.GetComponent<RectTransform>().rect.height;

        float botSize = botCanvasSize * _botCanvasC.transform.localScale.y;

        float tableroHeight = (Screen.height - (topSize + botSize));



        float cameraSizeHeight = (_mainCamera.orthographicSize * 2);
        float cameraSizeWidth = cameraSizeHeight * _mainCamera.aspect;
        
        float cameraSizePixelHeight = _mainCamera.pixelHeight;
        float cameraSizePixelWidth = _mainCamera.pixelWidth;

        Debug.Log("Altura camara en unidades: " + cameraSizeHeight + " Altura en pixeles: " + cameraSizePixelHeight);

        float ppuHeight = cameraSizePixelHeight / cameraSizeHeight;
        Debug.Log("Espacio para el tablero:" + tableroHeight + " Tamaño del tablero: " + tableroHeightUnidades * ppuHeight);
        float pixelesParaTablero = tableroHeightUnidades * ppuHeight;
        

        float unidadesTop = topSize / ppuHeight;
        float unidadesBot = botSize / ppuHeight;
        


        _mainCamera.orthographicSize = (((pixelesParaTablero + topSize + botSize) / ppuHeight) / 2);
        Debug.Log("Unidades alto camara: " + _mainCamera.orthographicSize * 2);
        Debug.Log("Tamaño canvas top: " + topSize);
        Debug.Log("Tamaño alto camara en pixeles: " + (pixelesParaTablero + topSize + botSize));

        float ppuWidth = _mainCamera.pixelWidth / ((_mainCamera.orthographicSize * 2) * _mainCamera.aspect);


        float cameraWidthUnits = _mainCamera.pixelWidth / ppuWidth;
        Debug.Log("Tamañano ancho camara en unidades: " + cameraWidthUnits);

        if(cameraWidthUnits < tableroWidthUnidades)
        {
            float newCameraHeightSize = tableroWidthUnidades / _mainCamera.aspect;
            _mainCamera.orthographicSize = (newCameraHeightSize / 2);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
