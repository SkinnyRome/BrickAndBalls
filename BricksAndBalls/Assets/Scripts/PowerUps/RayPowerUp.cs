using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ray PowerUp which creates 4 Ray tiles at random positions.
/// </summary>
public class RayPowerUp : PowerUp {

    public BoardManager _boardManager;
    public GameObject _hRay;
    public GameObject _vRay;

    public override void Init(uint uses)
    {
        base.Init(uses);
        _type = PowerUp_Type.PU_RAY;

    }

    public void OnClick()
    {

        if(_usesAvailables > 0)
        {
            Consume();
        }

    }

    public override void Consume()
    {
        base.Consume();

        _boardManager.CreateTileAtRandomPos(BasicTile.TILE_TYPE.HRAY, 2);
        _boardManager.CreateTileAtRandomPos(BasicTile.TILE_TYPE.VRAY, 2);
    }
}
