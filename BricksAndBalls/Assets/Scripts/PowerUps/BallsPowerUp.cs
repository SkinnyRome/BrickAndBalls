using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This PowerUp adds balls to the next shoot.
/// </summary>
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
        _levelManager.AddBallsThisShoot(BALLS_TO_ADD);

        
    }
}
