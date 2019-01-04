using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class FadeInTime : MonoBehaviour {

    public enum RenderType { SPRITE_RENDERER, IMAGE_RENDERER}
    public RenderType _type;
    public float fadeDuration;
    public float fadeTimes;
    private int _nFades;
    public bool loop = false;
    public bool fadeAtStart = false;
    private SpriteRenderer _spriteRenderer;
    private UnityEngine.UI.Image _imageRenderer;
    private bool _fadeDone;
    private Color _color;

    // Use this for initialization
    void Start () {

        _nFades = 0;

        if(_type == RenderType.SPRITE_RENDERER)
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
                Destroy(gameObject);
            else
            {
                _color = _spriteRenderer.color;
            }
        }
        else if(_type == RenderType.IMAGE_RENDERER)
        {
            _imageRenderer = gameObject.GetComponent<UnityEngine.UI.Image>();
            if (_imageRenderer == null)
                Destroy(gameObject);
            else
            {
                _color = _imageRenderer.color;
            }

        }

        _color.a = 0.0f;
        SetColor();

        _fadeDone = false;

        if (fadeAtStart)
            Fade();
        

	}
	
	public void Fade()
    {
        _nFades = 0;
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
      
        
        while( _nFades < fadeTimes || loop)
        {
            Debug.Log("Doing fade" + _fadeDone);
            yield return new WaitForSeconds(fadeDuration / 10.0f);
            if (!_fadeDone)
            {
                IncreaseAlpha();
              
            }
            else
            {
                DecreaseAlpha();
               
            }

        }

    }
    
    private void IncreaseAlpha()
    {
        _color.a += 0.1f;
        if (_color.a >= 1.0f)
        {
            _fadeDone = true;
        }

        SetColor();
    }

    private void DecreaseAlpha()
    {
        _color.a -= 0.1f;
        if (_color.a <= 0)
        {
            _color.a = 0.0f;
            _nFades++;
            _fadeDone = false;
        }
        SetColor();
    }

    private void SetColor()
    {
        if (_type == RenderType.IMAGE_RENDERER)
            _imageRenderer.color = _color;
        else if (_type == RenderType.SPRITE_RENDERER)
            _spriteRenderer.color = _color;

        Debug.Log("Seting color" + _color.a);
    }
}
