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

    private float tableroWidthUnidades = 11.5f;
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

        float parentCanvasHeight = _parentCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;


        float topCanvasSize = _topCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;
        l.SetTopCanvasSize(topCanvasSize);


        float botCanvasSize = _botCanvas.GetComponent<RectTransform>().rect.height * _parentCanvas.GetComponent<RectTransform>().localScale.y;
        l.SetBotCanvasSize(botCanvasSize);


        float canvasTableroSize = parentCanvasHeight - botCanvasSize - topCanvasSize;

        float relacionUnidades = canvasTableroSize / tableroHeightUnidades;

        float unidadesRalesTop = topCanvasSize / relacionUnidades;
        float unidadesRealesBot = botCanvasSize / relacionUnidades;

        Debug.Log("Unidades reales top: " + unidadesRalesTop);

        float tableroHeight = (Screen.height - (topCanvasSize + botCanvasSize));


        //Debug.Log("Espacio para el tablero:" + tableroHeight + " Tamaño del tablero: " + tableroHeightUnidades * ppuHeight);
        float pixelesParaTablero = tableroHeightUnidades * ppuHeight;

        float totalPixeles = pixelesParaTablero   + topCanvasSize  + botCanvasSize;
        float totalUnidades = totalPixeles / (ppuHeight);

        float unidadesTop = (topCanvasSize / (ppuHeight));
        float unidadesBot = botCanvasSize / (ppuHeight);

       
        
        _mainCamera.orthographicSize = ((tableroHeightUnidades + unidadesRalesTop + unidadesRealesBot)/ 2.0f );
        float yPosition = ((tableroHeightUnidades) / 2.0f + (unidadesRalesTop / 2.0f) - unidadesRealesBot/2.0f) ; 
     


        _mainCamera.transform.position = new Vector3(5.5f, yPosition, -10); 
    



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
