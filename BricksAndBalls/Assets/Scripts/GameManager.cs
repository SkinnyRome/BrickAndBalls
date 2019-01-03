using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists.
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private string _selectedLevelName;
    private uint _selectedLevelNumber;

    //private UnityAds _ads;

    private UserData _playerData;


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


        LoadData();
        /*
        _ads = GetComponent<UnityAds>();
        if(_ads != null)
        {
            Debug.Log("Getting Ads failed.");
        }
        */
    }

    public void RewardedForWatchingAd() {
        Debug.Log("PREMIADO POR VER UN VIDEO");
        _playerData.gems += 10;
    }

    public void DisplayRewardedAd()
    {
        //_ads.ShowRewardedAd();
    }

    public void LevelFinished(uint starsObtained) {

       //Update the level score
        _playerData.levels_stars[(int)_selectedLevelNumber] = starsObtained;
        
        //Add a new level to the player and to the array with 0 stars score, so the player can play it now.
        _playerData.levels_stars.Add(0);
        _playerData.current_level++;

        _playerData.total_stars += starsObtained;
        
        SaveData();
        //_ads.ShowBasicAd();
    }

    public void LoadLevel(uint mapIndex)
    {
        _selectedLevelName = "mapdata" + mapIndex.ToString();
        _selectedLevelNumber = mapIndex;

        SceneManager.LoadScene("GameplayScene");
    }

    public void RetryLevel()
    {
        if (_selectedLevelName != null)
            SceneManager.LoadScene("GameplayScene");
        else
            Debug.Log("No hay mapa del ultimo juego");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }


    public uint GetPlayerLevel()
    {
        return _playerData.current_level;
    }

  
    public string GetSelectedLevelName()
    {
        return _selectedLevelName;
    }
    
    public uint GetSelectedLevelNumber()
    {
        return _selectedLevelNumber;
    }

    public void SaveData()
    {
        FileSave saver = new FileSave();
        saver.SaveData(_playerData);
    }

    public void LoadData()
    {
        FileSave loader = new FileSave();
        /*DEBUG*/
        //loader.DeleteSavedData();     
        _playerData = loader.LoadData();
    }

    public UserData GetUserData()
    {
        return _playerData;
    }

    public void PurchasePowerUp(PowerUp_Type t, uint nGems, uint nPurchases = 1)
    {

        uint totalGems = nGems * nPurchases;

        if (totalGems <= _playerData.gems)
        {

            switch (t)
            {
                case PowerUp_Type.PU_RAY:
                    _playerData.powerUps.rays += nPurchases;
                    break;
            }

            _playerData.gems -= totalGems;
        }

        SaveData();
    }

}