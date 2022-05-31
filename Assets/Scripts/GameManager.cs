using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /* DECLARATIONS */

    public static GameManager Instance;
    // made to be referencable by any other script - it is static, i.e. every instance has the same values
    // there is a "get" accessor to make values read only, and "private set" to write within class
    // i.e. there is a "get" (read-access), but not a "set" (write-access) except for "private set" (write-access within it's class)
    // this is also known as Singleton structure

    public int timesPlayed = 1;

    // to use for session saving / loading
    public int previousSceneBuildIndex;
    public List<int> sceneHistory = new List<int>();

    public SessionData seshData = new SessionData(); // creates a new instance of class SessionData
    public GameData gameData = new GameData(); // creates a new instance of class GameData

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
        gameData = GameDataLoader();
        gameData.timesPlayed++;
    }

    [System.Serializable]
    public class SessionData
    {
        // class attributes
        public int hitPoints;
        public int musherAmount;
        public int invincibilityAmount;
        public int goldenAmount;
        public int toolkitAmount;
        public float level1Time;
        public float level2Time;
        public float level3Time;
    }
        
    [System.Serializable] // called to make class serializable i.e. turn from an object to bytes for storage; eventually, deserialized when used again (from bytes back to object)
    public class GameData // data to be saved between sessions in Json format - n.b Unity Json utility does not support arrays
    {
        public int timesPlayed;
        public int patsToTheDog;
        // Best Times
        public float level1BestTime;
        public float level2BestTime;
        public float level3BestTime;
        // Level 1 Ranks
        public int level1DiamondRanks;
        public int level1GoldRanks;
        public int level1SilverRanks;
        public int level1BronzeRanks;
        // Level 2 Ranks
        public int level2DiamondRanks;
        public int level2GoldRanks;
        public int level2SilverRanks;
        public int level2BronzeRanks;
        // Level 3 Ranks
        public int level3DiamondRanks;
        public int level3GoldRanks;
        public int level3SilverRanks;
        public int level3BronzeRanks;
        // Player Data
        public float playerEXP;
        public int playerLevel;
    }

    private void GameDataSaver(GameData gameData) // used to save data to a file
    {
        string json = JsonUtility.ToJson(gameData); // turns data into a json string

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // uses System.IO namespace to write to a consistent folder, with name savefile.json
        Debug.Log(json);
    }

    private GameData GameDataLoader()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // writes a string with the path file to check
        if (File.Exists(path)) // check if file exists
        {
            string json = File.ReadAllText(path); // reads file content to json string
            GameData gameData = JsonUtility.FromJson<GameData>(json); // reads json string data to variable data
            if(gameData.playerLevel == 0) { gameData.playerLevel++; } 
            return gameData;
        }
        else
        {
            GameData gameData = new GameData();
            return gameData;
        }
    }

    public void clearData()
    {
        GameData gameData = new GameData();
        Debug.Log(gameData);
        gameData.timesPlayed = 1;
        gameData.playerLevel = 1;
        GameDataSaver(gameData);
        Instance.gameData = GameDataLoader();
    }

    public void EndSceneDataSaver(SessionData seshData, int health, int mushAmount, int invincibilityAmount, int goldAmount, int toolsAmount)
    {
        seshData.hitPoints = health;
        seshData.musherAmount = mushAmount;
        seshData.invincibilityAmount = invincibilityAmount;
        seshData.goldenAmount = goldAmount;
        seshData.toolkitAmount = toolsAmount;
    }

    public int LevelNumberChecker() // UPDATE IF BUILD INDEX CHANGES
    {
        int levelNum = SceneManager.GetActiveScene().buildIndex / 2; // works because of build index structure where level number is 1/2 of build index
        return levelNum;
    }
    public bool HighScoreChecker(GameData gameData, SessionData seshData, int levelNum, float time) // returns a true if level high score is achieved
    {
        // Check for New High Score
        bool isNewHighScore = false;
        switch (levelNum) // sets session time
        {
            case 1:
                seshData.level1Time = time;
                break;
            case 2:
                seshData.level2Time = time;
                break;
            case 3:
                seshData.level3Time = time;
                break;
        }
        switch (levelNum) // checks for new all-time best for level
        {
            case 1:
                if (gameData.level1BestTime == 0f || gameData.level1BestTime > seshData.level1Time) 
                {
                    gameData.level1BestTime = seshData.level1Time;
                    isNewHighScore = true;
                }
                break;
            case 2:
                if (gameData.level2BestTime == 0f || gameData.level2BestTime > seshData.level2Time)
                {
                    gameData.level2BestTime = seshData.level2Time;
                    isNewHighScore = true;
                }
                break;
            case 3:
                if (gameData.level3BestTime == 0f || gameData.level3BestTime > seshData.level3Time)
                {
                    gameData.level3BestTime = seshData.level3Time;
                    isNewHighScore = true;
                }
                break;
        }
        GameDataSaver(gameData);
        return isNewHighScore; // used to trigger new record in game scene
    }

    //Call this whenever you want to load a new scene
    //It will add the new scene to the sceneHistory list
    public void LoadScene(int newScene)
    {
        sceneHistory.Add(newScene);
        Time.timeScale = 1;
        SceneManager.LoadScene(newScene);
    }

    private void OnApplicationQuit()
    {
        GameDataSaver(gameData);
        Debug.Log("Saved data");
    }
}