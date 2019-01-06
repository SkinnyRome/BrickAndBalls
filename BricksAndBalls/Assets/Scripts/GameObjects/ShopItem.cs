using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Asumimos que en el juego solo se pueden comprar PowerUps, si no esto habría que ampliarlo.
//Los atributos están publicos para poder manejarlos desde el Editor. De momento se queda así, a ver si hay tiempo.

/// <summary>
/// Class used for the GameObjects in the 'shop' UI in the main menu Scene. This GameObjects contain information about the PowerUp that can
/// be purchased and its prize.
/// </summary>
public class ShopItem : MonoBehaviour {

    //Public attributes setted in the Editor.
    public PowerUp_Type _powerUpType;   //Enum used for determinate the PowerUp
    public uint _prize;     //The prize of one PowerUP

    public MainMenuManager _menuManager;    //The MenuManager who also controls the shop.
    
    /// <summary>
    /// Method called when the Player pulse the button to buy our PowerUp. Call menuManager to complete the purchase.
    /// </summary>
    public void OnPurchase()
    {
        _menuManager.PurchasePowerUp(_powerUpType, _prize);
    }

}
