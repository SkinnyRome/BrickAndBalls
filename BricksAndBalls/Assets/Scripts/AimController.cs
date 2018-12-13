using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    private LevelManager _levelManager;
    private Vector3 _position;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN

        if (Input.GetMouseButton(0))
        {
            _position = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _levelManager.Shoot(_position);
            gameObject.SetActive(false);
        }


#endif
#if UNITY_ANDROID





#endif

    }


    public void Init(LevelManager lm) {
        _levelManager = lm;
    }
    
    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
