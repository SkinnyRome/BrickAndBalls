using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {


    private LevelManager _levelManager;
    public Canvas _endGameSuccessMenu;
    public Canvas _pauseMenu;
    public Canvas _gameCanvas;
    public Canvas _endGameFailedMenu;
    public Canvas _helpCanvas;
    public GameObject _freeReward;



    public void Init(LevelManager l)
    {
        _levelManager = l;
        _gameCanvas.gameObject.SetActive(true);
        _pauseMenu.gameObject.SetActive(false);
        _endGameSuccessMenu.gameObject.SetActive(false);
        _endGameFailedMenu.gameObject.SetActive(false);
        _helpCanvas.gameObject.SetActive(false);
    }

    public void Pause()
    {
        _gameCanvas.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
    }

    public void EndGameSuccess()
    {
        _gameCanvas.gameObject.SetActive(false);
        _endGameSuccessMenu.gameObject.SetActive(true);
        _freeReward.SetActive(true);
        
    }

    public void EndGameFailed()
    {
        _gameCanvas.gameObject.SetActive(false);
        _endGameFailedMenu.gameObject.SetActive(true);
    }

    public void ShowHelpCanvas()
    {
        _helpCanvas.gameObject.SetActive(true);

    }

    public void CloseHelpCanvas()
    {
        _helpCanvas.gameObject.SetActive(false);
    }

    public void Resume()
    {
        _pauseMenu.gameObject.SetActive(false);
        _gameCanvas.gameObject.SetActive(true);

    }
}
