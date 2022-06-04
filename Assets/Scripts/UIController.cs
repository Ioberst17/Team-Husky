using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;

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
    public Text levelNameHeaderText;
    public Text levelNameText;
    public Text readySetGoText;
    public Stopwatch Stopwatch;
    public PlayerController PlayerController; // assigned in inspector
    public GameManager gameManager;
    public LevelSystem levelSystem;
    public GameObject pauseMenu;
    public GameObject debugMenu;
    public GameObject endLevelMenu;
    public GameObject uiCanvas;

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

    // for end of level menu, found by UIController in Start Function
    // END OF LEVEL - LEVEL RESULTS UI
    public Text endOfLevelTime;
    // for Rank
    public Text endOfLevelRank;
    public GameObject endOfLevelRankDiamond;
    public GameObject endOfLevelRankGold;
    public GameObject endOfLevelRankSilver;
    public GameObject endOfLevelRankBronze;
    public int whichRankImageToRotate;
    public float rankSpriteRotation = -0.5F;
    // for Awards
    public Text endOfLevelAward;
    public GameObject endOfLevelAwardHealth; 
    public ParticleSystem endOfLevelHealthParticleSystem; //assigned in inspector
    public ParticleSystem endOfLevelHealthParticleSystem2; //assigned in inspector
    public ParticleSystem endOfLevelRankParticles; //assigned in inspector
    public ParticleSystem endOfLevelRankParticles2; //assigned in inspector
    // END OF LEVEL - PLAYER EXP
    public Slider playerEXP;
    public Text endOfLevelPlayerLevel;
    public Animator endOfLevelPlayerLevelTextAnimator;


    private int readySetGoTimer;
    [SerializeField] private Transform checkpoint1;
    [SerializeField] private Transform checkpoint2;

    public void Start()
    {
        gameManager = GameManager.Instance;
        levelSystem = PlayerController.gameObject.GetComponent<LevelSystem>();
        if(GameObject.Find("UI Canvas")) // if it is a playable level, i.e. has UI Canvas
        {
            uiCanvas = GameObject.Find("UI Canvas");
            endOfLevelRank = ObjectFinder.FindObject(uiCanvas, "LevelEndRankText").GetComponentInChildren<Text>();
            endOfLevelAward = ObjectFinder.FindObject(uiCanvas, "LevelEndAwardText").GetComponentInChildren<Text>();
            endOfLevelRankDiamond = ObjectFinder.FindObject(uiCanvas, "LevelEndRankImageDiamond");
            endOfLevelRankGold = ObjectFinder.FindObject(uiCanvas, "LevelEndRankImageGold");
            endOfLevelRankSilver = ObjectFinder.FindObject(uiCanvas, "LevelEndRankImageSilver");
            endOfLevelRankBronze = ObjectFinder.FindObject(uiCanvas, "LevelEndRankImageBronze");
            endOfLevelAwardHealth = ObjectFinder.FindObject(uiCanvas, "LevelEndAwardImageHealth");
            endOfLevelPlayerLevel = ObjectFinder.FindObject(uiCanvas, "EndOfLevelPlayerLevel").GetComponentInChildren<Text>();
            endOfLevelPlayerLevelTextAnimator = ObjectFinder.FindObject(uiCanvas, "EndOfLevelPlayerLevel").GetComponentInChildren<Animator>();
            endOfLevelPlayerLevel.text = gameManager.gameData.playerLevel.ToString();
        }

    }

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

    public void EndOfLevelUIUpdates() // called in player controller to ensure UI happens after Player Controller update game data / session 
    {
        if (PlayerController.levelComplete) // assist in updating end level menu
        {
            
            // update rank image by deactivating all the incorrect images and loop a shiny effect
            // rank name has been updated in a call in PlayerController at level complete
            if(endOfLevelRank.text == "Diamond") // if it is of one
            {
                endOfLevelRankGold.gameObject.SetActive(false); endOfLevelRankSilver.gameObject.SetActive(false); endOfLevelRankBronze.gameObject.SetActive(false); // then deactive others
                endOfLevelRankDiamond.gameObject.GetComponent<UIShiny>().Play(); // then play a shiny effect
                endOfLevelRankDiamond.gameObject.GetComponent<UIShiny>().effectPlayer.loop = true; // loop it
                whichRankImageToRotate = 1;
               
            }
            else if (endOfLevelRank.text == "Gold") 
            {
                endOfLevelRankDiamond.gameObject.SetActive(false); endOfLevelRankSilver.gameObject.SetActive(false); endOfLevelRankBronze.gameObject.SetActive(false);
                endOfLevelRankGold.gameObject.GetComponent<UIShiny>().Play(); 
                endOfLevelRankGold.gameObject.GetComponent<UIShiny>().effectPlayer.loop = true;
                whichRankImageToRotate = 2;
                
            }
            else if (endOfLevelRank.text == "Silver")
            {
                endOfLevelRankDiamond.gameObject.SetActive(false); endOfLevelRankGold.gameObject.SetActive(false); endOfLevelRankBronze.gameObject.SetActive(false);
                endOfLevelRankSilver.gameObject.GetComponent<UIShiny>().Play();
                endOfLevelRankSilver.gameObject.GetComponent<UIShiny>().effectPlayer.loop = true;
                whichRankImageToRotate = 3;
                
            }
            else
            {
                endOfLevelRankDiamond.gameObject.SetActive(false); endOfLevelRankGold.gameObject.SetActive(false); endOfLevelRankSilver.gameObject.SetActive(false);
                endOfLevelRankBronze.gameObject.GetComponent<UIShiny>().Play();
                endOfLevelRankBronze.gameObject.GetComponent<UIShiny>().effectPlayer.loop = true;
                whichRankImageToRotate = 4;

            }

            levelSystem.UpdateXPUI(10/25F);
        }
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
            //goldenSprite.transform.eulerAngles = new Vector3(0, 0, -17); //resets to 17 as an oddity of the current golden sprite, will change with Justin's new sprite
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
        if (PlayerController.gameState == "running" || PlayerController.gameState == "starting")
        {
            pauseMenu.SetActive(false);
            debugMenu.SetActive(false);
        }

        endLevelMenu.SetActive(PlayerController.levelComplete);
        if (PlayerController.levelComplete)
        {
            switch (whichRankImageToRotate) // used to rotate rank image
            {
                case 1:
                    endOfLevelRankDiamond.gameObject.transform.Rotate(new Vector3(0, rankSpriteRotation, 0));
                    break;
                case 2:
                    endOfLevelRankGold.transform.Rotate(new Vector3(0, rankSpriteRotation, 0));

                    break;
                case 3:
                    endOfLevelRankSilver.transform.Rotate(new Vector3(0, rankSpriteRotation, 0));
                    break;
                case 4:
                    endOfLevelRankBronze.transform.Rotate(new Vector3(0, rankSpriteRotation, 0));
                    break;
            }
            // play rank particles
            endOfLevelRankParticles.Play();
            endOfLevelRankParticles2.Play();


            // particles and rotation of health image
            endOfLevelAwardHealth.gameObject.transform.Rotate(new Vector3(0, -rankSpriteRotation, 0));
            endOfLevelHealthParticleSystem.Play();
            endOfLevelHealthParticleSystem2.Play();
        }
    }
}
