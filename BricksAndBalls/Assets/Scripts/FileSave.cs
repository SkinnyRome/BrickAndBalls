using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileSave : SaveInterface
{
    public UserData LoadData()
    {
        UserData data = new UserData();

        if (File.Exists(Application.persistentDataPath + "/playersave.data"))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream saveFile = File.Open(Application.persistentDataPath + "/playersave.data", FileMode.Open);
            try
            {
                data = (UserData)bf.Deserialize(saveFile);
            }
            catch (EndOfStreamException e)
            {
                Debug.Log("Los datos guardados son erroneos " +  e.Message);
                saveFile.Close();
                File.Delete(Application.persistentDataPath + "/playersave.data");
                data = new UserData();
            }
            saveFile.Close();
            Debug.Log("Game Loaded");

        }
        else
        {
            Debug.Log("No game saved!");
        }

        return data;
    }

    public void SaveData(UserData data)
    {
        UserData saveData = data;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playersave.data");
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game Saved");

    }

    public void DeleteSavedData()
    {
        if (File.Exists(Application.persistentDataPath + "/playersave.data"))
            File.Delete(Application.persistentDataPath + "/playersave.data");
    }

}
