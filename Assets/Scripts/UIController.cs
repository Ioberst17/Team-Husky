using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // NOTE: the handling of animation related to condition focused powerups (invincibility / golden) is generally handled the ConditionFocusedPowerUps.cs
    // It's in the Gameplay UI Support Folder of the Scripts Folder
    // That said, rotation of those UI sprites is handled below
    private int HP;
    public Slider healthBar;
    private int maxHP;
    public Text HPtext;
    public Text timerText;
    public Text readySetGoText;
    public Stopwatch Stopwatch;
    public PlayerController PlayerController; // assigned in inspector
    public GameObject pauseMenu;
    public GameObject debugMenu;
    public GameObject endLevelMenu;

    // for UI Images, assigned in inspectors
    public Image invincibilitySprite; 
    public Image goldenSprite; 
    public float spriteYRotation = -5;
    public ParticleSystem invincibilityShaderGlow;
    public ParticleSystem invincibilityShaderStars;


    //for New Highscore at end of level / all are assigned in inspector
    public Text newRecordText;
    public ParticleSystem newRecordParticle1; 
    public ParticleSystem newRecordParticle2;
    public ParticleSystem newRecordParticle3;

    private int readySetGoTimer;
    [SerializeField] private Transform checkpoint1;
    [SerializeField] private Transform checkpoint2;

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
    public void goToCheckpoint2()
    {
        PlayerController.rb.transform.position = checkpoint2.position;
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

    public void timeFormatter()
    {
        // formats time based on the fact Unity uses seconds as a basis for time.time (see Stopwatch.cs for methods)
        timerText.text = string.Format("{0:0}:{1:00}:{2:00}",
                                    Stopwatch.GetMinutes(),
                                    Stopwatch.GetSeconds() - 60 * Stopwatch.GetMinutes(),
                                    (Stopwatch.GetMilliseconds() * 100.00f) % 100.00f);
    }

    public void NewHighScoreDisplay()
    {
        newRecordText.gameObject.SetActive(true);
        newRecordParticle1.Play();
        newRecordParticle2.Play(); 
        newRecordParticle3.Play();
    }

    public void Update()
    {
        // manages invincibility UI icon rotation on use
        if (PlayerController.invincibilityOn)
        {
            invincibilitySprite.transform.Rotate(new Vector3(0, spriteYRotation, 0));
            invincibilityShaderGlow.gameObject.SetActive(true);
            invincibilityShaderStars.gameObject.SetActive(true);
            invincibilityShaderGlow.Play();
            invincibilityShaderStars.Play();
            
        }
        else
        {
            invincibilitySprite.transform.eulerAngles = (new Vector3(0, 0, 0));
            invincibilityShaderGlow.Stop();
            invincibilityShaderStars.Stop();
            invincibilityShaderGlow.gameObject.SetActive(false);
            invincibilityShaderStars.gameObject.SetActive(false);
        }

        // manages golden UI icon rotation on use
        if (PlayerController.goldenOn)
        {
            goldenSprite.transform.Rotate(new Vector3(0, spriteYRotation, 0));
        }
        else
        {
            goldenSprite.transform.eulerAngles = new Vector3(0, 0, -17); //resets to 17 as an oddity of the current golden sprite, will change with Justin's new sprite
        }

        if (PlayerController.gameState != "paused" && PlayerController.levelComplete !=  true)
        {
            Stopwatch.Unpause();
            timeFormatter();
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
