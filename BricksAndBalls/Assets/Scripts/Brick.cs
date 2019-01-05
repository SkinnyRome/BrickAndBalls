using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BasicTile {

    public TextMesh _textPrefab;
    private TextMesh _lifeText;
    private Color _spriteInitialColor;
    private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
		
	}
	
    public override void Init(uint life, Vector2 pos, GameObject father, LevelManager lm)
    {
        base.Init(life, pos, father, lm);
        _tileType = TILE_TYPE.BRICK;
        
        _needToBeDestroyed = true;

        TextMesh t = Object.Instantiate(_textPrefab, _textPrefab.transform.position, Quaternion.identity);
        t.transform.SetParent(gameObject.transform);
        t.transform.localPosition = new Vector3(0, 0, 0);
        t.characterSize = 0.035f;
        _lifeText = t;
        _lifeText.text = _life.ToString();

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteInitialColor = _spriteRenderer.color;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        DecreaseLife(1);
        FadeAnimation();
    }

    public override void DecreaseLife(uint i)
    {
        base.DecreaseLife(1);

        if(gameObject != null)
            _lifeText.text = _life.ToString();
        else
        {

            Debug.Log("muerto");
        }
    }

    private void FadeAnimation()
    {
        _spriteRenderer.color = Color.white;
        StartCoroutine(FadeCoroutine());

    }


    private IEnumerator FadeCoroutine()
    {
        Color diffColor = (Color.white - _spriteInitialColor) / 10;
        int i = 0;
        while(i < 10)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            _spriteRenderer.color -= diffColor;
            i++;
            
        }
        _spriteRenderer.color = _spriteInitialColor;
    }
}
