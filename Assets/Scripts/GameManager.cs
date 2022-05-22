using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameManager : MonoBehaviour
{
    /* DECLARATIONS */

    public static GameManager Instance { get; private set; }
    // made to be referencable by any other script - it is static, i.e. every instance has the same values
    // there is a "get" accessor to make values read only, and "private set" to write within class
    // i.e. there is a "get" (read-access), but not a "set" (write-access) except for "private set" (write-access within it's class)
    // this is also known as Singleton structure

    public int timesPlayed;
    public int currentSceneID;
    public float totalScore;
    public int totalTime;

    /* FUNCTIONS */

    void Awake() // manages creation and limiting of GameManager Instances
    {
        // GameManager instance mgmt.
        if (Instance != null) // checks for GameManager; no GameManager will be here on app load i.e. null, so condition not met at load
        {
            Destroy(gameObject); // if there is an existing GameManager it will be destroyed i.e. if going back to the menu screen
            return;
        }

        DontDestroyOnLoad(gameObject); // So the MainManager game object is not destroyed when scenes changes

        DataLoader();
        DataSaver();
    }

    [System.Serializable] // called to make class serializale i.e. turn from an object to bytes for storage; eventually, deserialized when used again (from bytes back to object)
    class SaveData // data to be saved between sessions in Json format
    {
        public int timesPlayed;
        public int currentSceneID;
        public float totalScore;
        public int totalTime;
    }

    public void DataSaver() // used to save data to a file
    {
        SaveData data = new SaveData(); // creates a new instance of class SaveData named data
        data.timesPlayed = timesPlayed; // adds to the new instance's current TeamColor attribute (which is of Type Color from UnityEngine namespace)

        string json = JsonUtility.ToJson(data); // turns data into a json string

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // uses System.IO namespace to write to a consistent folder, with name savefile.json
    }

    public void DataLoader()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // writes a string with the path file to check
        if (File.Exists(path)) // check if file exists
        {
            string json = File.ReadAllText(path); // reads file content to json string
            SaveData data = JsonUtility.FromJson<SaveData>(json); // reads json string data to variable data
            timesPlayed = data.timesPlayed; // adds class data back to used instance of timesPlayed
        }

        timesPlayed++;
        Debug.Log(timesPlayed);
    }
}