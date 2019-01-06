using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Script which add the fade in and fade out functionality to a gameobject if it have SpriteRenderer or UI Image.
/// </summary>
public class FadeInTime : MonoBehaviour {

    public enum RenderType { SPRITE_RENDERER, IMAGE_RENDERER}
    public RenderType _type;

    public float fadeDuration;  //Fade duration in seconds.
    public float fadeTimes;     //Times to do the fade.
    public bool loop = false;   //Remarks if the fade must be made in a loop or for a limited times.
    public bool fadeAtStart = false;    //Do fade at Start.

    private int _nFades;        //Number of fades done
    private SpriteRenderer _spriteRenderer; //The SpriteRenderer of the GO.
    private UnityEngine.UI.Image _imageRenderer;    //The UI Image of the GO.
    private bool _fadeInDone; //Boolean to mark if one fade in  has done.
    private Color _color;   //Private variable to store the color of the GO and control its alpha.

    void Start () {

        _nFades = 0;

        //Store the Image or Sprite of the GameObject. If it has none, delete the GameObject because it's an error.
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

        //Start with alpha at 0 to make it invisible.
        _color.a = 0.0f;
        SetColor();


        _fadeInDone = false;

        if (fadeAtStart)
            Fade();
        

	}
	
    /// <summary>
    /// Function which start the FadeCoroutine. It's called when the user want the gameobject to start fade.
    /// </summary>
	public void Fade()
    {
        _nFades = 0;
        StartCoroutine(FadeCoroutine());
    }

    /// <summary>
    /// Fade Coroutine
    /// </summary>
    private IEnumerator FadeCoroutine()
    {
      
        
        while( _nFades < fadeTimes || loop)
        {
            yield return new WaitForSeconds(fadeDuration / 10.0f);
            if (!_fadeInDone)
            {
                IncreaseAlpha();
              
            }
            else
            {
                DecreaseAlpha();
               
            }

        }

    }
    
    /// <summary>
    /// Increase the alpha of the GO by 0.1f
    /// </summary>
    private void IncreaseAlpha()
    {
        _color.a += 0.1f;
        if (_color.a >= 1.0f)
        {
            _fadeInDone = true;
        }

        SetColor();
    }

    /// <summary>
    /// Decrease the alpha of the GO by 0.1f
    /// </summary>
    private void DecreaseAlpha()
    {
        _color.a -= 0.1f;
        if (_color.a <= 0)
        {
            _color.a = 0.0f;
            _nFades++;
            _fadeInDone = false;
        }
        SetColor();
    }

    /// <summary>
    /// Set the current color (_color) to the gameobject
    /// </summary>
    private void SetColor()
    {
        if (_type == RenderType.IMAGE_RENDERER)
            _imageRenderer.color = _color;
        else if (_type == RenderType.SPRITE_RENDERER)
            _spriteRenderer.color = _color;

    }
}
