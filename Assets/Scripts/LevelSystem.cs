using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    //Attach to PlayerModel - it will handle end of level level ups
    public GameManager gameManager; // for gameManager.gameData.level and EXP data
    public PlayerController playerController; // need for gamestate
    public UIController uiController; // to pass updates to UI
    public Slider endOfLevelExperienceSlider; // assigned in inspector
    public float requiredXP;

    [Header("UI")]
    public Image frontXPSlider; // ASSIGN IN INSPECTOR

    //XP Bar Algo
    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 10;
    [Range(1f, 400f)]
    public float powerMultiplier = 1.1F;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerController = gameObject.GetComponent<PlayerController>();
        uiController = GameObject.FindObjectOfType<UIController>();
        //frontXPSlider.fillAmount = gameManager.gameData.playerEXP / requiredXP;
        requiredXP = CalculateRequiredXP();
    }

    public void UpdateXP(float updateSpeedMultiplier)
    {
        UpdateXPUI(updateSpeedMultiplier);
        if(gameManager.gameData.playerEXP >= requiredXP)
        {
            LevelUp();
        }
    }

    public void UpdateXPUI(float updateSpeedMultiplier)
    {
        float XPFraction;
        if (gameManager.gameData.playerEXP >= requiredXP)
        {
              XPFraction = 1;
        }
        else
        {
              XPFraction = gameManager.gameData.playerEXP / requiredXP;
        }

        endOfLevelExperienceSlider.value = Mathf.MoveTowards(endOfLevelExperienceSlider.value, XPFraction, updateSpeedMultiplier * Time.deltaTime);   
    }

    public void LevelUp() 
    {
        gameManager.gameData.playerLevel++; // raise player level in data
        uiController.endOfLevelPlayerLevel.text = gameManager.gameData.playerLevel.ToString();
        endOfLevelExperienceSlider.value = 0f; // reset EXP front slider image
        gameManager.gameData.playerEXP = Mathf.RoundToInt(gameManager.gameData.playerEXP - requiredXP); //carry over existing XP
        requiredXP = CalculateRequiredXP(); // get new EXP needed for next level
    }

    private int CalculateRequiredXP() // need to get the XP for next level
    {
        int solveForRequiredXP = 0;
        solveForRequiredXP = gameManager.gameData.playerLevel * 10;
        return solveForRequiredXP;
    }
}
