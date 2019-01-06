using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GameObject who manages the GamePlay canvases, showing or hiding them when it's necesary.
/// </summary>
public class CanvasManager : MonoBehaviour {


    private LevelManager _levelManager;
    public Canvas _endGameSuccessMenu;
    public Canvas _pauseMenu;
    public Canvas _gameCanvas;
    public Canvas _endGameFailedMenu;
    public Canvas _helpCanvas;
    public GameObject _powerUpCanvas;
    public GameObject _collectCanvas;
    public GameObject _freeReward;


    /// <summary>
    /// Initialize the Canvas Manager 
    /// </summary>
    /// <param name="l">The Level Manager GameObject</param>
    public void Init(LevelManager l)
    {
        _levelManager = l;
        _gameCanvas.gameObject.SetActive(true);
        _powerUpCanvas.SetActive(true);
        _collectCanvas.SetActive(false);
        _pauseMenu.gameObject.SetActive(false);
        _endGameSuccessMenu.gameObject.SetActive(false);
        _endGameFailedMenu.gameObject.SetActive(false);
        _helpCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show the pause menu
    /// </summary>
    public void Pause()
    {
        _gameCanvas.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
    }

    /// <summary>
    /// Show the succes end game menu
    /// </summary>
    public void EndGameSuccess()
    {
        _gameCanvas.gameObject.SetActive(false);
        _endGameSuccessMenu.gameObject.SetActive(true);
        _freeReward.SetActive(true);
        
    }

    /// <summary>
    /// Show the failed end game menu
    /// </summary>
    public void EndGameFailed()
    {
        _gameCanvas.gameObject.SetActive(false);
        _endGameFailedMenu.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Show the help canvas menu
    /// </summary>
    public void ShowHelpCanvas()
    {
        _helpCanvas.gameObject.SetActive(true);

    }

    /// <summary>
    /// Close the help canvas menu
    /// </summary>
    public void CloseHelpCanvas()
    {
        _helpCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Closes the pause menu
    /// </summary>
    public void Resume()
    {
        _pauseMenu.gameObject.SetActive(false);
        _gameCanvas.gameObject.SetActive(true);

    }

    /// <summary>
    /// Show the collect button and hide the PowerUps buttons
    /// </summary>
    public void OnThrowStarted()
    {
        _collectCanvas.SetActive(true);
        _powerUpCanvas.SetActive(false);
    }

    /// <summary>
    /// Show the PowerUp buttons and hide the collect Button
    /// </summary>
    public void OnThrowEnded()
    {
        _collectCanvas.SetActive(false);
        _powerUpCanvas.SetActive(true);
    }
}
