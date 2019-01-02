using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private uint _currentLevel;
    private string _selectedMapName;
    private List<uint> levelsScore;



    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);


    }

    void Start()
    {
        //Cargar los datos guardados.
        _currentLevel = 2;
    }

    public void GameOver() {


    }

    public void LevelFinished() {
        _currentLevel++;
    }

    public void LoadLevel(string mapName)
    {
        _selectedMapName = mapName;
        SceneManager.LoadScene("GameplayScene");
    }

    public void RetryCurrentLevel()
    {
        if (_selectedMapName != null)
            SceneManager.LoadScene("GameplayScene");
        else
            Debug.Log("No hay mapa del ultimo juego");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }


    public uint GetCurrentLevel()
    {
        return _currentLevel;
    }

  
    public string GetLevelNameSelected()
    {
        return _selectedMapName;
    }

   

    public void LoadNextLevel()
    {
        string mapName = "mapdata" + _currentLevel.ToString();
        LoadLevel(mapName);
    }
}