using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int HP; 
    public Text HPtext;
    public Text timerText;
    public Time timer;
    public PlayerController PlayerController;


    public void levelStart()
    {

        
        HP = PlayerController.HealthPoints;
        HPtext.text = HP.ToString();
    }
    public void updateHealth()
    {
        HP = PlayerController.HealthPoints;
        HPtext.text = HP.ToString();
    }
    private void FixedUpdate()
    {
        //timerText.text = timer.ToString();
    }
}
