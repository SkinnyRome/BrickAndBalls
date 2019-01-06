using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The interface of the class used to store and load the data of the player.
/// </summary>
public interface SaveInterface {

    /// <summary>
    /// Saves the data in the device.
    /// </summary>
    /// <param name="data">The player information to store</param>
	void SaveData(UserData data);

    /// <summary>
    /// Loads the data of the player if it's exist. Either, it creates a new one.
    /// </summary>
    /// <returns>The loaded player data or a new one.</returns>
    UserData LoadData();
}
