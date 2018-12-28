using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevelButton : MonoBehaviour {


    string _mapString;
    public TextMesh _textPrefab;


    public void OnClick()
    {
        GameManager.instance.LoadLevel(_mapString);
    }

    public void Init(string m)
    {
        _mapString = m;
        string mapNumber = m.Substring(7);
        TextMesh t = Object.Instantiate(_textPrefab, _textPrefab.transform.position, Quaternion.identity);
        t.transform.SetParent(gameObject.transform);
        t.transform.localPosition = new Vector3(0, 0, 0);
        t.text = mapNumber;
        t.characterSize = 0.03f;
        
    }
}
