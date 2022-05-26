using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;
using UnityEngine.UI;

public class InvincibilityUI : MonoBehaviour
{
    public UIGradient gradientUI;
    public UIShiny shinyUI;
    public PlayerController playerController;
    public Image fillColor;
    public static float gradientIncrement = 2F;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        fillColor = gameObject.GetComponent<Image>();
        shinyUI = gameObject.GetComponent<UIShiny>();
        gradientUI = gameObject.GetComponent<UIGradient>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.invincibilityOn)
        {
            Changer();
        }
        else
        {
            Return();
        }
    }

    private void FixedUpdate() // used to rotate underlying gradient
    {
        GradientModifier();
    }

    public void Changer()
    {
        if(fillColor.color != new Color32(255, 255, 255, 255))
        {
            fillColor.color = new Color32(255, 255, 255, 255); // set health bar to white

            //set colors of gradient
            gradientUI.color1 = new Color32(255, 0, 82, 255); 
            gradientUI.color2 = new Color32(0, 255, 29, 255);
            gradientUI.color3 = new Color32(0, 179, 255, 255);
            gradientUI.color4 = new Color32(255, 0, 247, 255);

            shinyUI.Play(); // turn on shiny animation
            shinyUI.Loop(true); // loop animation
        }
    }

    public void Return()
    {
        fillColor.color = new Color32(0, 215, 60, 255); //set UI back to green

        //set gradient back to 0
        gradientUI.color1 = new Color32(255, 255, 255, 255);
        gradientUI.color2 = new Color32(255, 255, 255, 255);
        gradientUI.color3 = new Color32(255, 255, 255, 255);
        gradientUI.color4 = new Color32(255, 255, 255, 255);

        shinyUI.Loop(false); // turn off looping
        shinyUI.Stop(); //turn off shiny
    }

    public void GradientModifier()
    {
        if (playerController.invincibilityOn)
        {
            if (gradientUI.rotation <= 179)
            {
                gradientUI.rotation -= gradientIncrement;
            }
            else if (gradientUI.rotation >= -179)
            {
                gradientUI.rotation += gradientIncrement;
            }
            else
            {

            }
        }
    }
}
