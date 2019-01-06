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



/// <summary>
/// Class which stores the player progress in the game.
/// </summary>
[System.Serializable]
public class UserData {

    private const uint MAX_USES = 100;  //Max uses of the PowerUp

    public uint current_level = 1; //The level of the player. It indicates which is the last level that the player has unlocked.
    public uint gems = 150; //The virtual coin of the game.
    public uint total_stars = 0;    //The total stars gained by the player
    public PowerUpsAmount powerUps = new PowerUpsAmount();  //The Amount of PowerUp that the player has
    public List<uint> levels_stars = new List<uint>() { 0, 0}; //The stars gained in each level by the player 

    /// <summary>
    /// Consume 'nUses' uses of the PowerUp 't'
    /// </summary>
    /// <param name="t">The type of the PowerUp which has been used</param>
    /// <param name="nUses">The uses (normally 1) consumed</param>
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

    /// <summary>
    /// Add uses to the PowerUp when the player purchases it.
    /// </summary>
    /// <param name="t">The type of the PowerUp that the player has bought</param>
    /// <param name="nPurchases">The quantity of uses bought</param>
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
