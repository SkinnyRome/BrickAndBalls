using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevelButton : MonoBehaviour {


    uint _mapIndex;
    public TextMesh _textPrefab;
    

    void Start()
    {
        //_lightRenderer = transform.Find("Light").gameObject.GetComponent<SpriteRenderer>();

    }

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

        LoadStars();
        
    }

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
