using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerSupport : MonoBehaviour
{
    // used for buttons, finds GameManager and loads its functions in scenes
    // this is because otherwise, buttons that link to GameManager would not be able to find the reference to GameManager if added in the hierarchy and then in play mode
    // It additionally fires screen transitions (as it is involved in loading scenes and manages animations of images and calling audio controller)

    public GameManager gameManager;
    public Animator screenTransition;
    public MusicController musicController;
    public GameObject canvas; // used to update scores on main menu

    //scores to update if needed
    public GameObject playerLevel;
    public GameObject timesPlayed;
    public GameObject patsToTheDog;
    public GameObject level1BestTime;
    public GameObject level2BestTime;
    public GameObject level3BestTime;
    public GameObject L1Diamonds;
    public GameObject L1Gold;
    public GameObject L1Silver;
    public GameObject L1Bronze;
    public GameObject L2Diamonds;
    public GameObject L2Gold;
    public GameObject L2Silver;
    public GameObject L2Bronze;
    public GameObject L3Diamonds;
    public GameObject L3Gold;
    public GameObject L3Silver;
    public GameObject L3Bronze;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        screenTransition = GameObject.Find("ScreenTransitions").GetComponent<Animator>();
        musicController = GameObject.Find("MusicController").GetComponent<MusicController>();

        if (SceneManager.GetActiveScene().buildIndex == 0) // needed to ensure data records UI get's loaded on the main menu
        {
            canvas = GameObject.Find("Canvas"); // since canvas is active on load, it can be found in the hierarchy using Find

            // inactive objects including the hidden records page on main menu load, can't be found using .Find-related functions
            // as a workaround created a static class search function (in ObjectFinder.cs) to loop through hierarchy and find the relevant fields and assign them for use
            playerLevel = ObjectFinder.FindObject(canvas, "PlayerLevel");
            timesPlayed = ObjectFinder.FindObject(canvas, "TimesPlayed");
            patsToTheDog = ObjectFinder.FindObject(canvas, "DogPats");
            level1BestTime = ObjectFinder.FindObject(canvas, "Level1BestTime");
            level2BestTime = ObjectFinder.FindObject(canvas, "Level2BestTime");
            level3BestTime = ObjectFinder.FindObject(canvas, "Level3BestTime");
            L1Diamonds = ObjectFinder.FindObject(canvas, "Level1Diamonds");
            L1Gold = ObjectFinder.FindObject(canvas, "Level1Gold");
            L1Silver = ObjectFinder.FindObject(canvas, "Level1Silver");
            L1Bronze = ObjectFinder.FindObject(canvas, "Level1Bronze");
            L2Diamonds = ObjectFinder.FindObject(canvas, "Level2Diamonds");
            L2Gold = ObjectFinder.FindObject(canvas, "Level2Gold");
            L2Silver = ObjectFinder.FindObject(canvas, "Level2Silver");
            L2Bronze = ObjectFinder.FindObject(canvas, "Level2Bronze");
            L3Diamonds = ObjectFinder.FindObject(canvas, "Level3Diamonds");
            L3Gold = ObjectFinder.FindObject(canvas, "Level3Gold");
            L3Silver = ObjectFinder.FindObject(canvas, "Level3Silver");
            L3Bronze = ObjectFinder.FindObject(canvas, "Level3Bronze");

            // once data fields are found, do an update using game session data
            dataRecordsUIUpdate();
        }
    }

    public void gameManagerLoader(int sceneID) // load scene function from gameManager
    {
        StartCoroutine(ScreenTransition(sceneID)); // trigger scene transition
    }

    IEnumerator ScreenTransition(int sceneID) 
    {
        screenTransition.ResetTrigger("EndScreenTransitionMainMenu");
        screenTransition.SetTrigger("EndScreenTransitionMainMenu");
        musicController.MainMenuToPlayGameDogBark();
        if (musicController.MusicSource.isPlaying)
        {
            Debug.Log("It works");
        }
        yield return new WaitForSecondsRealtime(1.5f);
        gameManager.LoadScene(sceneID); // call load scene AFTER scene transition happens
    }

    public void gameManagerDataClear() // clear data function from gameManager
    {
        gameManager.clearData();
        dataRecordsUIUpdate();
    }

    public void petTheDogUIUpdate()
    {
        gameManager.gameData.patsToTheDog += 1;
        patsToTheDog.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.patsToTheDog.ToString(); //update times played 
    }

    public string timeFormatterForUIRecordsDisplay(float toManipulate)// returns time record as Minutes and Seconds vs. long float
    {
        var time = TimeSpan.FromSeconds(toManipulate); /*string.Format("{0:0}:{1:00}", // formats as minutes : seconds
                                                (int)toManipulate / 60F, // returns minutes
                                                (int)toManipulate * ((int)toManipulate / 60F)); // returns seconds*/
        var stringToReturn = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        return stringToReturn;
    }

    public void dataRecordsUIUpdate()
    {
        playerLevel.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.playerLevel.ToString(); //update player level
        timesPlayed.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.timesPlayed.ToString(); //update times played
        patsToTheDog.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.patsToTheDog.ToString(); //update pats given
        // update best level times
        if (gameManager.gameData.level1BestTime == 0) //update level 1 score in UI
        {
            level1BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "N/A"; //if it's 0, there isn't a time registed - list it as N/A
        }
        else
        {
            var stringToUse = timeFormatterForUIRecordsDisplay(gameManager.gameData.level1BestTime); // else pull in the relevant number
            level1BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = stringToUse; // assign it to the relevant text in hierarchy
        }

        if (gameManager.gameData.level2BestTime == 0) //update level 2 score in UI
        {
            level2BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "N/A";
        }
        else
        {
            var stringToUse = timeFormatterForUIRecordsDisplay(gameManager.gameData.level2BestTime); 
            level2BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = stringToUse;
        }

        if (gameManager.gameData.level3BestTime == 0) //update level 3 score in UI
        {
            level3BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "N/A"; ;
        }
        else
        {
            var stringToUse = timeFormatterForUIRecordsDisplay(gameManager.gameData.level3BestTime);
            level3BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = stringToUse;
        }
        //update Level 1 scores
        L1Diamonds.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level1DiamondRanks.ToString();
        L1Gold.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level1GoldRanks.ToString();
        L1Silver.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level1SilverRanks.ToString();
        L1Bronze.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level1BronzeRanks.ToString();
        //update Level 2 scores
        L2Diamonds.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level2DiamondRanks.ToString();
        L2Gold.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level2GoldRanks.ToString();
        L2Silver.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level2SilverRanks.ToString();
        L2Bronze.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level2BronzeRanks.ToString();
        //update Level 3 scores
        L3Diamonds.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level3DiamondRanks.ToString();
        L3Gold.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level3GoldRanks.ToString();
        L3Silver.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level3SilverRanks.ToString();
        L3Bronze.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level3BronzeRanks.ToString();
    }

}