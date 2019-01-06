using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enumerator to diferenciate the PowerUp type.
public enum PowerUp_Type {NONE, PU_RAY, PU_BALLS, PU_ELIMINATE_ROW};

/// <summary>
/// Father class of the PowerUps. Its have the common functionality for all of them.
/// </summary>
public class PowerUp : MonoBehaviour {

    protected uint _usesAvailables; //The uses availables of the PowerUp
    protected PowerUp_Type _type;   //The type of the PowerUp

    private Text _usesText; //The text displayed in the game

    /// <summary>
    /// Initialize the PowerUp
    /// </summary>
    /// <param name="uses">The uses number</param>
    public virtual void Init(uint uses)
    {
        _usesAvailables = uses;
        GameObject g = gameObject.transform.Find("UsesText").gameObject;
        _usesText = gameObject.transform.Find("UsesText").gameObject.GetComponent<Text>();
        if (_usesText == null)
            Debug.Log("No se ha encontrado el texto");
        else
        {
            _usesText.text = "x " + _usesAvailables;
        }

    }

    /// <summary>
    /// Use the PowerUp and consume one use. The childs implementations have the functionality of each one.
    /// </summary>
    public virtual void Consume()
    {
        _usesAvailables--;
        _usesText.text = "x " + _usesAvailables;
        GameManager.instance.ConsumePowerUp(_type);


    }
}
