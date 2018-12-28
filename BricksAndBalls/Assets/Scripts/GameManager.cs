using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private uint _currentLevel;
    private string _selectedMapName;



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

    }

    public void LoadLevel(string mapName)
    {
        _selectedMapName = mapName;
        SceneManager.LoadScene("GameplayScene");
    }

    //Update is called every frame.
    void Update()
    {

    }

    public uint GetCurrentLevel()
    {
        return _currentLevel;
    }

   

    public string GetLevelNameSelected()
    {
        return _selectedMapName;
    }
}