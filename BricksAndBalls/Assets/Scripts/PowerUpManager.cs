using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    private LevelManager _levelManager;
    public PowerUp _rayPowerUp;



	
	public void Init (LevelManager l) {

        _levelManager = l;
        UserData data = GameManager.instance.GetUserData();

        _rayPowerUp.Init(data.powerUps.rays);
	}
	

}
