using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {


    public MenuLoader _menuLoader;
    public UnityEngine.UI.Text _starText;
    public UnityEngine.UI.Text _gemText;

    // Use this for initialization
    void Start () {
        _menuLoader.Init(GameManager.instance.GetPlayerLevel());

        UserData data = GameManager.instance.GetUserData();

        _starText.text = data.total_stars.ToString();
        _gemText.text = data.gems.ToString();
	}
	
	
}
