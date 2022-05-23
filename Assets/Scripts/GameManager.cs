using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    /* DECLARATIONS */

    public static GameManager Instance;
    // made to be referencable by any other script - it is static, i.e. every instance has the same values
    // there is a "get" accessor to make values read only, and "private set" to write within class
    // i.e. there is a "get" (read-access), but not a "set" (write-access) except for "private set" (write-access within it's class)
    // this is also known as Singleton structure

    // Game Data for long-term storage
    public int timesPlayed;
    public int currentSceneID;
    public float totalScore;
    public int totalTime;

    // Session Data
    // gameplay
    public int hitPoints;
    public int musherAmount;
    public int invincibilityAmount;
    public int goldenAmount;
    public int toolkitAmount;
    // scores
    public float level1Time;
    public float level2Time;
    public float level3Time;
    // to use for session saving / loading
    public int previousSceneBuildIndex;
    public List<int> sceneHistory = new List<int>();

    /* FUNCTIONS */

    void Awake() // manages creation and limiting of GameManager Instances
    {
        // GameManager instance mgmt.
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }

        sceneHistory.Add(0);

        GameDataLoader();
        GameDataSaver();
    }

    public class SessionData
    {
        public int hitPoints;
        public int musherAmount;
        public int invincibilityAmount;
        public int goldenAmount;
        public int toolkitAmount;
    }
    
    
    [System.Serializable] // called to make class serializale i.e. turn from an object to bytes for storage; eventually, deserialized when used again (from bytes back to object)
    class GameData // data to be saved between sessions in Json format
    {
        public int timesPlayed;
        public int currentSceneID;
        public float totalScore;
        public int totalTime;
    }

    public void GameDataSaver() // used to save data to a file
    {
        GameData data = new GameData(); // creates a new instance of class GameData named data
        data.timesPlayed = timesPlayed; // adds to the new instance's current TeamColor attribute (which is of Type Color from UnityEngine namespace)

        string json = JsonUtility.ToJson(data); // turns data into a json string

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // uses System.IO namespace to write to a consistent folder, with name savefile.json
    }

    public void GameDataLoader()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // writes a string with the path file to check
        if (File.Exists(path)) // check if file exists
        {
            string json = File.ReadAllText(path); // reads file content to json string
            GameData data = JsonUtility.FromJson<GameData>(json); // reads json string data to variable data
            timesPlayed = data.timesPlayed; // adds class data back to used instance of timesPlayed
        }

        timesPlayed++;
        Debug.Log(timesPlayed);
    }

    public void EndSceneDataSaver(int health, int mushAmount, int invincAmount, int goldAmount, int toolsAmount, int levelNum, float time)
    {
        hitPoints = health;
        musherAmount = mushAmount;
        invincibilityAmount = invincAmount;
        goldenAmount = goldAmount;
        toolkitAmount = toolsAmount;
        switch (levelNum)
        {
            case 0:
                break;
            case 1:
                level1Time = time;
                break;
            case 2:
                level2Time = time;
                break;
            case 3:
                level3Time = time;
                break;
        }

    }

    //Call this whenever you want to load a new scene
    //It will add the new scene to the sceneHistory list
    public void LoadScene(int newScene)
    {
        sceneHistory.Add(newScene);
        SceneManager.LoadScene(newScene);
    }
}