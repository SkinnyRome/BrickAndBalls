using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {


    public MenuLoader _menuLoader;
    public UnityEngine.UI.Text _starText;
    public UnityEngine.UI.Text _gemText;
    private UnityEngine.UI.Text _shopGemText;
    public Canvas _shopMenu;

    // Use this for initialization
    void Start () {
        _menuLoader.Init(GameManager.instance.GetPlayerLevel());
        _shopGemText = _shopMenu.transform.Find("Gems").Find("GemsText").gameObject.GetComponent<UnityEngine.UI.Text>();
        UpdateUI();
        _shopMenu.gameObject.SetActive(false);


	}

    public void ShowShop()
    {
        _shopMenu.gameObject.SetActive(true);
        //UpdateUI();
    }

    public void CloseShop()
    {
        _shopMenu.gameObject.SetActive(false);
    }

    public void PurchasePowerUp(PowerUp_Type t, uint p)
    {
        GameManager.instance.PurchasePowerUp(t,p);
        UpdateUI();
    }

    private void UpdateUI()
    {
        UserData data = GameManager.instance.GetUserData();

        _starText.text = data.total_stars.ToString();
        _gemText.text = data.gems.ToString();
        _shopGemText.text = _gemText.text;
    }
	
	
}
