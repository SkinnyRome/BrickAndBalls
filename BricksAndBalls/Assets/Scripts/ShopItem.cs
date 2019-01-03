using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Asumimos que en el juego solo se pueden comprar PowerUps, si no esto habría que ampliarlo.
//Los atributos están publicos para poder manejarlos desde el Editor. De momento se queda así, a ver si hay tiempo.
public class ShopItem : MonoBehaviour {

    public PowerUp_Type _powerUpType;
    public uint _prize;
    public MainMenuManager _menuManager;
    


    public ShopItem(uint p, PowerUp_Type t)
    {
        _prize = p;
        _powerUpType = t;
    }

    public void OnPurchase()
    {
        _menuManager.PurchasePowerUp(_powerUpType, _prize);
    }

}
