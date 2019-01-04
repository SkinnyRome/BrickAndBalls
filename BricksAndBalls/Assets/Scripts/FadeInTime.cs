using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInTime : MonoBehaviour {

    public float fadeDuration;
    public float fadeTimes;
    private UnityEngine.UI.Image _sprite;
    private bool _fadeDone;

    // Use this for initialization
    void Start () {

        _sprite = gameObject.GetComponent<UnityEngine.UI.Image>();
        if (_sprite == null)
            Destroy(gameObject);

        Color alphaZero = _sprite.color;
        alphaZero.a = 0.0f;

        _sprite.color = alphaZero;

        _fadeDone = false;

	}
	
	public void Fade()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        int n = 0;

        while( n < fadeTimes)
        {
            yield return new WaitForSeconds(fadeDuration / 10.0f);
            if (!_fadeDone)
            {
                IncreaseAlpha();
                if (_sprite.color.a >= 1.0f)
                    _fadeDone = true;
            }
            else
            {
                DecreaseAlpha();
                if(_sprite.color.a <= 0)
                {
                    n++;
                    _fadeDone = false;
                }
            }

        }

    }
    
    private void IncreaseAlpha()
    {
        Color alphaZero = _sprite.color;
        alphaZero.a += 0.1f;
        _sprite.color = alphaZero;

    }

    private void DecreaseAlpha()
    {
        Color alphaZero = _sprite.color;
        alphaZero.a -= 0.1f;
        if (alphaZero.a < 0.0f)
            alphaZero.a = 0.0f;
        _sprite.color = alphaZero;
    }
}
