using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public struct PowerUpsAmount
{
    public uint rays;
    public uint balls;
    public uint eliminateRows;

}

[System.Serializable]
public class UserData {

    private const uint MAX_USES = 100;

    public uint current_level = 1;
    public uint gems = 150;
    public uint total_stars = 0;
    public PowerUpsAmount powerUps = new PowerUpsAmount();
    public List<uint> levels_stars = new List<uint>() { 0, 0};

    public void ConsumePowerUp(PowerUp_Type t, uint nUses = 1)
    {
        switch (t)
        {
            case PowerUp_Type.PU_RAY:
                if (powerUps.rays >= nUses)
                    powerUps.rays -= nUses;
                break;
            case PowerUp_Type.PU_BALLS:
                if (powerUps.balls >= nUses)
                    powerUps.balls -= nUses;
                break;
            case PowerUp_Type.PU_ELIMINATE_ROW:
                if (powerUps.eliminateRows >= nUses)
                    powerUps.eliminateRows -= nUses;
                break;
            default:
                break;
        }
    }

    public void AddPowerUp(PowerUp_Type t, uint nPurchases)
    {

            switch (t)
            {
                case PowerUp_Type.PU_RAY:
                    powerUps.rays += nPurchases;
                    if (powerUps.rays > MAX_USES)
                        powerUps.rays = MAX_USES;
                    break;
                case PowerUp_Type.PU_BALLS:
                    powerUps.balls += nPurchases;
                    if (powerUps.balls > MAX_USES)
                        powerUps.balls = MAX_USES;
                break;
                case PowerUp_Type.PU_ELIMINATE_ROW:
                    powerUps.eliminateRows += nPurchases;
                    if (powerUps.eliminateRows > MAX_USES)
                        powerUps.eliminateRows = MAX_USES;
                break;
                default:
                    break;
            }

    }

}
