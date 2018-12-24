using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BasicTile {

    public TextMesh _textPrefab;
    private TextMesh _lifeText;

	// Use this for initialization
	void Start () {
		
	}
	
    public void Init(uint life, Vector2 pos, GameObject father, LevelManager lm)
    {
        _levelManager = lm;
        _row = (uint)pos.x;
        _column = (uint)pos.y;
        transform.parent = father.transform;        
        gameObject.transform.localPosition = pos;        
        _life = life;

        TextMesh t = Object.Instantiate(_textPrefab, _textPrefab.transform.position, Quaternion.identity);
        t.transform.SetParent(gameObject.transform);
        t.transform.localPosition = new Vector3(0, 0, 0);
        _lifeText = t;
        _lifeText.text = _life.ToString();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        DecreaseLife(1);
    }

    public override void DecreaseLife(uint i)
    {
        base.DecreaseLife(1);

        if(gameObject != null)
            _lifeText.text = _life.ToString();
        else
        {

            Debug.Log("meurto");
        }



    }
}
