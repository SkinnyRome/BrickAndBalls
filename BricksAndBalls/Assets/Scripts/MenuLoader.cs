using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour {


    public Camera _mainCamera;
    public Canvas _canvas;
    public UnityEngine.UI.Button _button;

    // Use this for initialization
    void Start () {


        Canvas.ForceUpdateCanvases();

        float canvasWidth = _canvas.GetComponent<RectTransform>().rect.width;
        float width = canvasWidth * _canvas.transform.localScale.x;

        float canvasHeight = _canvas.GetComponent<RectTransform>().rect.height;
        float height = canvasHeight * _canvas.transform.localScale.y;

        Debug.Log("Ancho: " + width +  " Alto: " + height);
        
        float pixelsCasillaAncho = width / 5;
        float pixelsCasillaAlto = height / 5;

        Debug.Log("Casilla ancho: " + pixelsCasillaAncho + " Casilla alto: " + pixelsCasillaAlto);


     


        //_button.GetComponent<RectTransform>().sca
        
                for( int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                    var button = Instantiate(_button, new Vector3(0, 0, 0), Quaternion.identity) as UnityEngine.UI.Button;
                    var rectTransform = button.GetComponent<RectTransform>();
                    button.transform.localPosition = new Vector3(0,0, -1);
                    rectTransform.SetParent(_canvas.transform);
                    rectTransform.pivot = Vector2.zero;
                    button.transform.localScale = new Vector3( rectTransform.rect.width / pixelsCasillaAncho, rectTransform.rect.width / pixelsCasillaAncho, 1);
                    button.transform.localPosition = new Vector3(i * pixelsCasillaAncho, j * pixelsCasillaAlto, -1);
                    rectTransform.anchorMax = Vector2.zero;
                    rectTransform.anchorMin = Vector2.zero;
            }
        }
                
        Canvas.ForceUpdateCanvases();



    }


}



