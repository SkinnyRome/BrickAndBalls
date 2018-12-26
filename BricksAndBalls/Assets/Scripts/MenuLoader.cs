using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour {


    public Camera _mainCamera;
    public Canvas _canvas;

	// Use this for initialization
	void Start () {


        float canvasWidth = _canvas.GetComponent<RectTransform>().rect.width;
        float width = canvasWidth * _canvas.transform.localScale.x;

        float canvasHeight = _canvas.GetComponent<RectTransform>().rect.height;
        float height = canvasHeight * _canvas.transform.localScale.y;

        Debug.Log("Ancho: " + width +  " Alto: " + height);

        float pixelsCasillaAncho = width / 5;
        float pixelsCasillaAlto = height / 5;

        Debug.Log("Casilla ancho: " + pixelsCasillaAncho + " Casilla alto: " + pixelsCasillaAlto);

       // float tableroHeight = (Screen.height - (topSize + botSize));



        float cameraSizeHeight = (_mainCamera.orthographicSize * 2);
        float cameraSizeWidth = cameraSizeHeight * _mainCamera.aspect;

        float camerHeightUnitsNeeded = 5 /_mainCamera.aspect;

        Debug.Log("Unidades de ancho: " + camerHeightUnitsNeeded);

        _mainCamera.orthographicSize = (camerHeightUnitsNeeded / 2);

        

        /*
        float cameraSizePixelHeight = _mainCamera.pixelHeight;
        float cameraSizePixelWidth = _mainCamera.pixelWidth;

        Debug.Log("Altura camara en unidades: " + cameraSizeHeight + " Altura en pixeles: " + cameraSizePixelHeight);

        float ppuHeight = cameraSizePixelHeight / cameraSizeHeight;
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

        if (cameraWidthUnits < tableroWidthUnidades)
        {
            float newCameraHeightSize = tableroWidthUnidades / _mainCamera.aspect;
            _mainCamera.orthographicSize = (newCameraHeightSize / 2);
        }

        Destroy(gameObject);*/
    }


}



