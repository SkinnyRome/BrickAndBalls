using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ANOTACIÓN: al final no hemos implementado el guardado con Player Prefs porque utilizando la ruta de archivos (FileSave) funciona,
//y como PlayerPrefs normalmente se utiliza solo para guardar los ajustes de juego hemos decidido no implementarla por falta de tiempo.

/// <summary>
/// Implementation of the SaveInterface using the PlayerPrefs of unity.
/// </summary>
public class PlayerPrefsSave : SaveInterface {
    public UserData LoadData()
    {
        UserData data = new UserData();





        return data;
    }

    public void SaveData(UserData data)
    {

        


    }

    
}
