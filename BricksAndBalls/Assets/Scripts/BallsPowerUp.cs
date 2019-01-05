using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPowerUp : PowerUp {

    public LevelManager _levelManager;
    private const int BALLS_TO_ADD = 10;
    

    public override void Init(uint uses)
    {
        base.Init(uses);
        _type = PowerUp_Type.PU_BALLS;

    }

    public void OnClick()
    {

        if (_usesAvailables > 0)
        {
            Consume();
        }

    }

    public override void Consume()
    {
        base.Consume();
        _levelManager.AddBallsThisMatch(BALLS_TO_ADD);

        
    }
}
