using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour {

    protected uint _usesAvailables;

    public const uint MAX_USES = 99;

    private Text _usesText;

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
            //_usesText.fontStyle =FontStyle.Bold;
        }

    }


    public void AddUses(uint newUses)
    {
        _usesAvailables += newUses;
        if (_usesAvailables > MAX_USES)
            _usesAvailables = MAX_USES;
    }

    public virtual void Consume()
    {
        _usesAvailables--;
        _usesText.text = "x " + _usesAvailables;

    }
}
