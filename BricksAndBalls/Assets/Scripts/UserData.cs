using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public struct PowerUpsAmount
{
    public uint rays;

}

[System.Serializable]
public class UserData {

    public uint current_level = 1;
    public uint gems = 0;
    public uint total_stars = 0;
    public PowerUpsAmount powerUps = new PowerUpsAmount();
    public List<uint> levels_stars = new List<uint>() { 0, 0};

	
}
