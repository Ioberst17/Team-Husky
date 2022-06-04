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
    public ParticleSystem expStars; // grabbed in start and should be a grandchild of the object
    public GameObject canvas;
    public float expGlowSizeHeight; // to scale glow
    public float requiredXP;

    [Header("UI")]
    public Image frontXPSlider; // ASSIGN IN INSPECTOR

    //XP Bar Algo
    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 50;
    [Range(1f, 400f)]
    public float powerMultiplier = 1.5F;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerController = gameObject.GetComponent<PlayerController>();
        uiController = GameObject.FindObjectOfType<UIController>();
        canvas = GameObject.Find("UI Canvas");
        expStars = ObjectFinder.FindObject(canvas, "EXPStars").GetComponentInChildren<ParticleSystem>(); 
        //frontXPSlider.fillAmount = gameManager.gameData.playerEXP / requiredXP;
        requiredXP = CalculateRequiredXP();
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


        StartCoroutine(GUILevelUp(XPFraction, updateSpeedMultiplier));
    }

    public void LevelUp(float updateSpeedMultiplier) 
    {
        gameManager.gameData.playerLevel++; // raise player level in data
        uiController.endOfLevelPlayerLevel.text = gameManager.gameData.playerLevel.ToString();
        uiController.endOfLevelPlayerLevelTextAnimator.SetTrigger("Scale");
        endOfLevelExperienceSlider.value = 0f; // reset EXP front slider image
        gameManager.gameData.playerEXP = Mathf.RoundToInt(gameManager.gameData.playerEXP - requiredXP); //carry over existing XP
        requiredXP = CalculateRequiredXP(); // get new EXP needed for next level
        if(gameManager.gameData.playerEXP >= 0) { UpdateXPUI(updateSpeedMultiplier); }
        expStars.Stop();
    }

    private int CalculateRequiredXP() // need to get the XP for next level
    {
        int solveForRequiredXP = 0;
        for (int levelCycle = 1; levelCycle <= gameManager.gameData.playerLevel; levelCycle++) // basically a Runscape leveling algorithm, key values are listed in variables section
        { solveForRequiredXP += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier)); };
        return solveForRequiredXP;
    }
    IEnumerator GUILevelUp(float XPFraction, float updateSpeedMultiplier)
    {
        while (endOfLevelExperienceSlider.value != XPFraction)
        {
            endOfLevelExperienceSlider.value = Mathf.MoveTowards(endOfLevelExperienceSlider.value, XPFraction, updateSpeedMultiplier * Time.deltaTime);

            if (!expStars.isPlaying) { expStars.Play(); }
            //expGlowSizeHeight = endOfLevelExperienceSlider.value * 10;
            yield return null;
        }
        
        
        if (XPFraction == 1)
        {
            LevelUp(updateSpeedMultiplier);
        }
        expStars.Stop();
        yield return null;
    }
}
