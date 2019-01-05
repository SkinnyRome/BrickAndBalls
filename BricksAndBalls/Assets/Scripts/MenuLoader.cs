using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class MenuLoader : MonoBehaviour {


    public Camera _mainCamera;
    public Canvas _canvasButtons;
    public Canvas _topCanvas;
    public Canvas _botCanvas;
    public UnityEngine.UI.Button _lockedButton;
    public UnityEngine.UI.Button _unlockedButton;
    private MainMenuManager _menuManager;


    private const string mapFileName = "mapdata";

    // Use this for initialization
    public void Init(MainMenuManager menuManager, uint currentLevel) {

        _menuManager = menuManager;
        Canvas.ForceUpdateCanvases();
        
        float topCanvasSize = _topCanvas.GetComponent<RectTransform>().rect.height * _topCanvas.transform.parent.localScale.y;
        float botCanvasSize = _botCanvas.GetComponent<RectTransform>().rect.height * _botCanvas.transform.parent.localScale.y;

        _menuManager.SetCanvasSize(topCanvasSize, botCanvasSize);

        _mainCamera.orthographicSize = (5.5f / _mainCamera.aspect) / 2;

       // _mainCamera.transform.Translate(new Vector3(0, botCanvasSize, 0));

        float cameraSizeHeight = (_mainCamera.orthographicSize * 2);
        float cameraSizeWidth = cameraSizeHeight * _mainCamera.aspect;

        float cameraSizePixelHeight = _mainCamera.pixelHeight;
        float cameraSizePixelWidth = _mainCamera.pixelWidth;


        float ppuHeight = cameraSizePixelHeight / cameraSizeHeight;
        float ppuWidth = cameraSizePixelWidth / cameraSizeWidth;

        float canvasBotUnits = botCanvasSize / ppuHeight;

        List<uint> maps = new List<uint>();

        Object[] files = Resources.LoadAll("Maps", typeof(TextAsset));
        
        foreach (TextAsset file in files)
        {
            if (!file.name.Contains("gamedata"))
            {
                string name = file.name.Split('.')[0];
                uint number = uint.Parse(name.Substring(7));
                maps.Add(number);
            }
        }

        maps.Sort();

        int totalRows = maps.Count / 5;
        float canvasYPosition = totalRows / 2;

        Debug.Log("Numero de filas: " + totalRows);
        
        _canvasButtons.GetComponent<RectTransform>().sizeDelta = new Vector2(5 , totalRows);
        _canvasButtons.GetComponent<RectTransform>().position = new Vector3(_canvasButtons.GetComponent<RectTransform>().position.x, _canvasButtons.GetComponent<RectTransform>().position.y + ((totalRows/2) - 3), _canvasButtons.GetComponent<RectTransform>().position.z);
        //_mainCamera.transform.position = new Vector3(_canvasButtons.GetComponent<RectTransform>().position.x, (_canvasButtons.GetComponent<RectTransform>().position.y) - canvasBotUnits, -10);
        


        int j = 0;
        int m = 0;
        while (m < maps.Count)
        {
            int i = 0;
            while ( i < 5 && m < maps.Count)
            {
                UnityEngine.UI.Button button = null;

                float xPos = _canvasButtons.GetComponent<RectTransform>().sizeDelta.x;
                //_canvasButtons.GetComponent<RectTransform>().sizeDelta = new Vector2(xPos ,j);

                if (m < currentLevel)
                {
                    button = Instantiate(_unlockedButton, new Vector3(0, 0, 0), Quaternion.identity) as UnityEngine.UI.Button;
                    button.GetComponent<MapLevelButton>().Init(maps[m]);
                }
                else
                {
                    button = Instantiate(_lockedButton, new Vector3(0, 0, 0), Quaternion.identity) as UnityEngine.UI.Button;
                }
                RectTransform rectTransform = button.GetComponent<RectTransform>();
                rectTransform.SetParent(_canvasButtons.transform);
                rectTransform.pivot = Vector2.zero;
                button.transform.localPosition = new Vector3(i, j, -1);
                rectTransform.anchorMax = Vector2.zero;
                rectTransform.anchorMin = Vector2.zero;

                m++;
                i++;
            }
            j += 1;
            //m++;
        }


                
        Canvas.ForceUpdateCanvases();



    }


}



