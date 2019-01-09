using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BasicTile {

    public TextMesh _textPrefab;
    private TextMesh _lifeText;
    private Color _spriteInitialColor;
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Initialize the tile and its attributes
    /// </summary>
    /// <param name="life">The life of the brick</param>
    /// /// <param name="pos">The position</param>
    /// <param name="father">The BoarManager GameObject</param>
    /// <param name="lm">The LevelManager GameObject</param>
    public override void Init(int life, Vector2 pos, GameObject father, LevelManager lm)
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
    /// <summary>
    /// Notify a collision
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        DecreaseLife(1);
        FadeAnimation();
    }
    /// <summary>
    /// Decrease the life of the tile
    /// </summary>
    /// <param name="i">Amount of life to be decreased</param>
    public override void DecreaseLife(int i)
    {
        base.DecreaseLife(1);

        if(gameObject != null)
            _lifeText.text = _life.ToString();
        else
        {

            Debug.Log("muerto");
        }
    }
    /// <summary>
    /// Starts the fade animation
    /// </summary>
    private void FadeAnimation()
    {
        _spriteRenderer.color = Color.white;
        StartCoroutine(FadeCoroutine());

    }

    /// <summary>
    /// Fade coroutine
    /// </summary>
    /// <returns></returns>
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
