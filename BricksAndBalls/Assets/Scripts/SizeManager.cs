using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeManager : MonoBehaviour {

    public GameObject  _botCanvas;
    public GameObject _topCanvas;
    public Canvas _parentCanvas;
    public Canvas _topCanvasC;
    public Canvas _botCanvasC;
    public Camera _mainCamera;

    private float tableroWidthUnidades = 12.0f;
    private float tableroHeightUnidades = 14.0f;

	// Use this for initialization
	public void Init (LevelManager l) {

        Canvas.ForceUpdateCanvases();


        float cameraSizeHeight = (_mainCamera.orthographicSize * 2);
        float cameraSizeWidth = cameraSizeHeight * _mainCamera.aspect;
        
        float cameraSizePixelHeight = _mainCamera.pixelHeight;
        float cameraSizePixelWidth = _mainCamera.pixelWidth;

        //Debug.Log("Altura camara en unidades: " + cameraSizeHeight + " Altura en pixeles: " + cameraSizePixelHeight);

        float ppuHeight = cameraSizePixelHeight / cameraSizeHeight;
        float ppuWidth = cameraSizePixelHeight / cameraSizeWidth;

        Debug.Log("Top size sin scale: " + _topCanvas.GetComponent<RectTransform>().rect.height);
        Debug.Log("Local scale: " + _parentCanvas.GetComponent<RectTransform>().localScale.y);

        float topCanvasSize = _topCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;
        l.SetTopCanvasSize(topCanvasSize);

        float botCanvasSize = _botCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;
        l.SetBotCanvasSize(botCanvasSize);

        float tableroHeight = (Screen.height - (topCanvasSize + botCanvasSize));


        //Debug.Log("Espacio para el tablero:" + tableroHeight + " Tamaño del tablero: " + tableroHeightUnidades * ppuHeight);
        float pixelesParaTablero = tableroHeightUnidades * ppuHeight;

        float totalPixeles = pixelesParaTablero   + topCanvasSize  + botCanvasSize;
        float totalUnidades = totalPixeles / (ppuHeight);

        float unidadesTop = (topCanvasSize / (ppuHeight));
        float unidadesBot = botCanvasSize / (ppuHeight);

        Debug.Log("PPU height: " + ppuHeight + " PPU width: " + ppuWidth);
        Debug.Log("Tamaño top: " + unidadesTop + " Tamaño bot: " + unidadesBot);
        Debug.Log("Total unidades: " + totalUnidades);
        
        _mainCamera.orthographicSize = ((tableroHeightUnidades / 2) + (unidadesTop / 2) + (unidadesBot / 2));
        float yPosition = ((tableroHeightUnidades) / 2) + (unidadesTop) - (unidadesBot); 
     


        _mainCamera.transform.position = new Vector3(5.5f, yPosition, -10); 
        Debug.Log("Unidades alto camara: " + _mainCamera.orthographicSize * 2);
       // Debug.Log("Tamaño canvas top: " + topSize);
        //Debug.Log("Tamaño alto camara en pixeles: " + (pixelesParaTablero + topSize + botSize));



        float cameraWidthUnits = (_mainCamera.orthographicSize * 2) * _mainCamera.aspect;
       // Debug.Log("Tamañano ancho camara en unidades: " + cameraWidthUnits);

        if(cameraWidthUnits < tableroWidthUnidades)
        {
            float newCameraHeightSize = tableroWidthUnidades / _mainCamera.aspect;
            _mainCamera.orthographicSize = (newCameraHeightSize / 2);
            
        }

        Destroy(gameObject);
	}
	

}
