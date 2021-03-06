﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Button class for the main menu buttons, which have information about it's level.
/// </summary>
public class MapLevelButton : MonoBehaviour {


    uint _mapIndex;     //The map number of this button.
    public TextMesh _textPrefab;    //Text prefab
    

    /// <summary>
    /// Function who call GameManager to charge a new level with our's map number
    /// </summary>
    public void OnClick()
    {
        GameManager.instance.LoadLevel(_mapIndex);
    }

    /// <summary>
    /// Initialize the button with its attributtes
    /// </summary>
    /// <param name="m">The map number</param>
    public void Init(uint m)
    {

        _mapIndex = m;
        string mapNumber = m.ToString();
        TextMesh t = Object.Instantiate(_textPrefab, _textPrefab.transform.position, Quaternion.identity);
        t.transform.SetParent(gameObject.transform);
        t.transform.localPosition = new Vector3(0, 0, 0);
        t.text = mapNumber;
        t.characterSize = 0.03f;

        LoadStars();
        
    }

    /// <summary>
    /// This function obtain information about the player to set the stars gained by him in this level.
    /// </summary>
    private void LoadStars()
    {
        uint stars = GameManager.instance.GetUserData().levels_stars[(int)_mapIndex];

        GameObject s1 = gameObject.transform.Find("Star1").gameObject;
        GameObject s2 = gameObject.transform.Find("Star2").gameObject;
        GameObject s3 = gameObject.transform.Find("Star3").gameObject;


        switch (stars)
        {
            case 0:
                break;
            case 1:
                s1.SetActive(true);
                break;
            case 2:
                s1.SetActive(true);
                s2.SetActive(true);
                break;
            case 3:
                s1.SetActive(true);
                s2.SetActive(true);
                s3.SetActive(true);
                break;
        }
    }
}
