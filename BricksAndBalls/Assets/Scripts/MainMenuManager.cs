using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {


    public MenuLoader _menuLoader;

	// Use this for initialization
	void Start () {
        _menuLoader.Init(GameManager.instance.GetCurrentLevel());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
