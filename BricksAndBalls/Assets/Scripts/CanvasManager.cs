using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    private LevelManager _levelManager;
    public PowerUp[] _powerUps;



	
	public void Init (LevelManager l) {

        _levelManager = l;

		foreach(PowerUp p in _powerUps)
        {
            //TODO: habrá que pedirle al GameManager los usos disponibles de cada powerUP
            p.Init(2);
        }
	}
	

}
