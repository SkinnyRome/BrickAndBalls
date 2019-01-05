using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateRowPowerUp : PowerUp {

    public BoardManager _boardManager;


    public override void Init(uint uses)
    {
        base.Init(uses);
        _type = PowerUp_Type.PU_ELIMINATE_ROW;

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

        _boardManager.DestroyLastRow();
    }
}
