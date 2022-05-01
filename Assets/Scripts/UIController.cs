using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int HP; 
    public Text HPtext;
    public Text timerText;
    public Stopwatch Stopwatch;
    public PlayerController PlayerController;
    public GameObject pauseMenu;


    public void levelStart()
    {
        Stopwatch.Begin();
        
        HP = PlayerController.HealthPoints;
        HPtext.text = HP.ToString();
        timerText.text = "Go";
    }
    public void updateHealth()
    {
        HP = PlayerController.HealthPoints;
        HPtext.text = HP.ToString();
    }
    public void Update()
    {
        if(PlayerController.gameState != "paused" && PlayerController.levelComplete !=  true)
        {
            Stopwatch.Unpause();
            timerText.text = Stopwatch.GetMinutes().ToString() + ":" + (Stopwatch.GetSeconds()  + Mathf.Round(Stopwatch.GetMilliseconds()*100.0f)*0.01f -(60*Stopwatch.GetMinutes())).ToString();
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


    }
}