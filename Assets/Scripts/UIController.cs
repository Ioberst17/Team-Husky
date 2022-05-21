using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int HP;
    public Slider healthBar;
    private int maxHP;
    public Text HPtext;
    public Text timerText;
    public Text readySetGoText;
    public Stopwatch Stopwatch;
    public PlayerController PlayerController;
    public GameObject pauseMenu;
    public GameObject debugMenu;
    public GameObject endLevelMenu;
    private int readySetGoTimer;
    [SerializeField] private Transform checkpoint1;
    


    public void levelStart()
    {
        healthBar.maxValue = PlayerController.HPSliderMax;
        HP = PlayerController.HealthPoints;
        HPtext.text = HP.ToString();
        timerText.text = "Go";
    }
    public void toggleDebugMenu()
    {
        if (debugMenu.activeSelf)
        {
            debugMenu.SetActive(false);
        }
        else
        {
            debugMenu.SetActive(true);
        }
    }
    public void goToCheckpoint1()
    {
        PlayerController.rb.transform.position = checkpoint1.position;
        toggleDebugMenu();

    }
    public void timerStart()
    {
        Stopwatch.Begin();
    }
        public void updateHealth()
    {
        HP = PlayerController.HealthPoints;
        healthBar.value = HP;
        HPtext.text = HP.ToString();
    }
    public void Update()
    {
        if(PlayerController.gameState != "paused" && PlayerController.levelComplete !=  true)
        {
            Stopwatch.Unpause();

            // formats time based on the fact Unity uses seconds as a basis for time.time (see Stopwatch.cs for methods)
            timerText.text = string.Format("{0:0}:{1:00}:{2:00}",
                                                Stopwatch.GetMinutes(),
                                                Stopwatch.GetSeconds() - 60 * Stopwatch.GetMinutes(),
                                                (Stopwatch.GetMilliseconds()*100.00f)%100.00f);
        }
        if (PlayerController.levelComplete == true)
        {
            Stopwatch.Pause();
        }
        if (PlayerController.gameState == "paused")
        {
            Stopwatch.Pause();
            pauseMenu.SetActive(true);
        }
        if (PlayerController.gameState == "running")
        {
            pauseMenu.SetActive(false);
        }

        endLevelMenu.SetActive(PlayerController.levelComplete);
    }
}
