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
    public GameObject timesPlayed;
    public GameObject patsToTheDog;
    public GameObject level1BestTime;
    public GameObject level2BestTime;
    public GameObject level3BestTime;


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
            timesPlayed = ObjectFinder.FindObject(canvas, "TimesPlayed");
            patsToTheDog = ObjectFinder.FindObject(canvas, "DogPats");
            level1BestTime = ObjectFinder.FindObject(canvas, "Level1BestTime");
            level2BestTime = ObjectFinder.FindObject(canvas, "Level2BestTime");
            level3BestTime = ObjectFinder.FindObject(canvas, "Level3BestTime");

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

    public void dataRecordsUIUpdate()
    {
        timesPlayed.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.timesPlayed.ToString(); //update times played
        patsToTheDog.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.patsToTheDog.ToString(); //update pats given
        if (gameManager.gameData.level1BestTime == 0) //update level 1 score in UI
        {
            level1BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "N/A"; //if it's 0, there isn't a time registed - list it as N/A
        }
        else
        {
            level1BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level1BestTime.ToString(); // else pull in the relevant number
        }

        if (gameManager.gameData.level2BestTime == 0) //update level 2 score in UI
        {
            level2BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "N/A";
        }
        else
        {
            level2BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level2BestTime.ToString();
        }

        if (gameManager.gameData.level3BestTime == 0) //update level 3 score in UI
        {
            level3BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "N/A"; ;
        }
        else
        {
            level3BestTime.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.gameData.level3BestTime.ToString();
        }
    }

}