using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Script for the GameObject who takes care of the main menu canvases and input, like the levelManager does for the GamePlayScene.
/// It shows and hide the UI items when its needed and also manage the shop.
/// </summary>
public class MainMenuManager : MonoBehaviour {


    public MenuLoader _menuLoader;  //The GameObject which instantiates the maps buttons
    public UnityEngine.UI.Text _starText;   //The star text
    public UnityEngine.UI.Text _gemText;    //The gem text
    private UnityEngine.UI.Text _shopGemText;   //The gem text of the shop
    public Canvas _shopMenu;    //The canvas for the shop
    public MainMenuInput _menuInputManager; //The input manager
    
    //Sizes of the canvas
    private float botCanvasSize;
    private float topCanvasSize;

    // Use this for initialization
    void Start () {
        _menuLoader.Init(this, GameManager.instance.GetPlayerLevel());
        _shopGemText = _shopMenu.transform.Find("Gems").Find("GemsText").gameObject.GetComponent<UnityEngine.UI.Text>();
        UpdateUI();
        _shopMenu.gameObject.SetActive(false);
        _menuInputManager.Init(this,botCanvasSize, topCanvasSize);

	}

    /// <summary>
    /// Show the shop menu
    /// </summary>
    public void ShowShop()
    {
        _shopMenu.gameObject.SetActive(true);
        //UpdateUI();
    }

    /// <summary>
    /// Close the shop menu
    /// </summary>
    public void CloseShop()
    {
        _shopMenu.gameObject.SetActive(false);
    }

    /// <summary>
    /// Purchase a PowerUp by calling the GameManager.
    /// </summary>
    /// <param name="t">The type of PowerUP</param>
    /// <param name="p">The prize</param>
    public void PurchasePowerUp(PowerUp_Type t, uint p)
    {
        GameManager.instance.PurchasePowerUp(t,p);
        UpdateUI();
    }

    /// <summary>
    /// Updates the displayed UI
    /// </summary>
    private void UpdateUI()
    {
        UserData data = GameManager.instance.GetUserData();

        _starText.text = data.total_stars.ToString();
        _gemText.text = data.gems.ToString();
        _shopGemText.text = _gemText.text;
    }
	
    /// <summary>
    /// Set the canvas size of the top and bottom canvas to control the input.
    /// </summary>
    /// <param name="top">Top canvas size in pixels</param>
    /// <param name="bot">Bot canvas size in pixels</param>
    public void SetCanvasSize(float top, float bot)
    {
        topCanvasSize = top;
        botCanvasSize = bot;
    }
	
}
