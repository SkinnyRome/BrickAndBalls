using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevelButton : MonoBehaviour {


    uint _mapIndex;
    public TextMesh _textPrefab;


    public void OnClick()
    {
        GameManager.instance.LoadLevel(_mapIndex);
    }

    public void Init(uint m)
    {
        _mapIndex = m;
        string mapNumber = m.ToString();
        TextMesh t = Object.Instantiate(_textPrefab, _textPrefab.transform.position, Quaternion.identity);
        t.transform.SetParent(gameObject.transform);
        t.transform.localPosition = new Vector3(0, 0, 0);
        t.text = mapNumber;
        t.characterSize = 0.03f;
        
    }
}
