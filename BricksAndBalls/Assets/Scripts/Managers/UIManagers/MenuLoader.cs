using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// This GameObjects puts all the level buttons in the MainMenu Scene correctly to make it all visibles by swipping the ButtonCanvas
/// </summary>
public class MenuLoader : MonoBehaviour {


    public Camera _mainCamera;
    public Canvas _canvasButtons;
    public Canvas _topCanvas;
    public Canvas _botCanvas;
    public UnityEngine.UI.Button _lockedButton;
    public UnityEngine.UI.Button _unlockedButton;
    private MainMenuManager _menuManager;


    private const string mapFileName = "mapdata";

    /// <summary>
    /// Initialize the GameObject
    /// </summary>
    /// <param name="menuManager">Main Menu Manager</param>
    /// <param name="currentLevel">The current level of the player to set correctly the buttons</param>
    public void Init(MainMenuManager menuManager, uint currentLevel) {

        Canvas.ForceUpdateCanvases();
        _menuManager = menuManager;
        
        //Get the top and bot canvas size to set it in the MenuManager
        float topCanvasSize = _topCanvas.GetComponent<RectTransform>().rect.height * _topCanvas.transform.parent.localScale.y;
        float botCanvasSize = _botCanvas.GetComponent<RectTransform>().rect.height * _botCanvas.transform.parent.localScale.y;
        _menuManager.SetCanvasSize(topCanvasSize, botCanvasSize);

        //Set the camera widht size in units 5.5, cause we want that the player can always see 5 buttons per row
        _mainCamera.orthographicSize = (5.5f / _mainCamera.aspect) / 2;

        //Get camera size in units
        float cameraSizeHeight = (_mainCamera.orthographicSize * 2);
        float cameraSizeWidth = cameraSizeHeight * _mainCamera.aspect;

        //Get camera size in pixels
        float cameraSizePixelHeight = _mainCamera.pixelHeight;
        float cameraSizePixelWidth = _mainCamera.pixelWidth;

        //Calculate pixels per unit
        float ppuHeight = cameraSizePixelHeight / cameraSizeHeight;
        float ppuWidth = cameraSizePixelWidth / cameraSizeWidth;

        float canvasBotUnits = botCanvasSize / ppuHeight;

        float cameraOffsetY = ((cameraSizeHeight / 2) - canvasBotUnits)  /*+(currentLevel / 5)*/;

        //Now, we load all the maps resources to make a list with all of them
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

        //Sort the array
        maps.Sort();

        //Calculate the rows that we will need
        int totalRows = maps.Count / 5;
        
        //Set the button canvas width to 5 and height to totalRows
        _canvasButtons.GetComponent<RectTransform>().sizeDelta = new Vector2(5 , totalRows);

        //Move the canvas button in the Y Axis to set it correctly
        _canvasButtons.GetComponent<RectTransform>().position = new Vector3(_canvasButtons.GetComponent<RectTransform>().position.x,
            _canvasButtons.GetComponent<RectTransform>().position.y + ((totalRows/2)),
            _canvasButtons.GetComponent<RectTransform>().position.z);
        

        //Now create all the button GameObject and set them to theri position in the canvas
        int j = 0;
        int m = 0;
        while (m < maps.Count)
        {
            int i = 0;
            while ( i < 5 && m < maps.Count)
            {
                UnityEngine.UI.Button button = null;

                float xPos = _canvasButtons.GetComponent<RectTransform>().sizeDelta.x;

                //If the map level is lower that the player level, it means that the player already have unlocked that map,
                //so we instantiate an unlocked button with the information of the map and initialize it
                if (m < currentLevel)
                {
                    button = Instantiate(_unlockedButton, new Vector3(0, 0, 0), Quaternion.identity) as UnityEngine.UI.Button;
                    button.GetComponent<MapLevelButton>().Init(maps[m]);
                }
                //Either, it means that the player has not reached this level yet, so we instantiate a locked button
                else
                {
                    button = Instantiate(_lockedButton, new Vector3(0, 0, 0), Quaternion.identity) as UnityEngine.UI.Button;
                }

                //Set the correct position in the canvas
                RectTransform rectTransform = button.GetComponent<RectTransform>();
                rectTransform.SetParent(_canvasButtons.transform);
                rectTransform.pivot = new Vector2(-0.25f, 0);
                button.transform.localPosition = new Vector3(i, j, -1);
                rectTransform.anchorMax = Vector2.zero;
                rectTransform.anchorMin = Vector2.zero;

                m++;
                i++;
            }
            j += 1;
        }


         //No se si es necesario actualizar el canvas aquí
        Canvas.ForceUpdateCanvases();

        _mainCamera.transform.Translate(0, cameraOffsetY, 0);
        Destroy(gameObject);
    }


}



