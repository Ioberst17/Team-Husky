using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;
using UnityEngine.UI;

public class InvincibilityShaderGradient : MonoBehaviour
{
    // ATTACHED TO HP SHADER TO TRIGGER ITS INVINCIBILITY GRADIENT

    public UIGradient gradientUI;
    public UIShiny shinyUI;
    public PlayerController playerController;
    public Image fillColor;
    public static float gradientRotationIncrement = 2F;
    public int rotationBouncer = 179; // if 179, headed up to 179; if -179, headed down to -179
    public static float gradientOffsetIncrement = 1F;
    public int offsetBouncer = 1; // if 1, headed up to 1; if -1, headed down to -1

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
            gradientUI.color1 = new Color32(0, 82, 231, 255); // font dark blue
            gradientUI.color2 = new Color32(255, 255, 255, 255); // white
            gradientUI.color3 = new Color32(0, 179, 255, 255); // light blue similar to invincibility sprite
            gradientUI.color4 = new Color32(255, 255, 255, 255); // white

            shinyUI.Play(); // turn on shiny animation
            shinyUI.effectPlayer.loop = true;
        }
    }

    public void Return()
    {
        fillColor.color = new Color32(48, 124, 238, 255); //set UI back to original color

        //set gradient back to 0
        gradientUI.color1 = new Color32(255, 255, 255, 255);
        gradientUI.color2 = new Color32(255, 255, 255, 255);
        gradientUI.color3 = new Color32(255, 255, 255, 255);
        gradientUI.color4 = new Color32(255, 255, 255, 255);

        shinyUI.Stop(); //turn off shiny
        shinyUI.effectPlayer.loop = false;
    }

    public void GradientModifier()
    {
        if (playerController.invincibilityOn) // Mod rotation of gradient
        {
            if (gradientUI.rotation <= 179)
            {
                gradientUI.rotation -= gradientRotationIncrement;
            }
            else if (gradientUI.rotation >= -179)
            {
                gradientUI.rotation += gradientRotationIncrement;
            }

            // reset bouncer
            if (gradientUI.rotation >= 179) { rotationBouncer = -179; }
            else if (gradientUI.rotation <- -179) { offsetBouncer = 179; }

        }
        if (playerController.invincibilityOn)
        {
            if (gradientUI.offset < .9 && offsetBouncer == 1) // mod offset of gradient
            {
                gradientUI.offset += gradientOffsetIncrement;
            }
            else if (gradientUI.rotation > -.9 & offsetBouncer == 0)
            {
                gradientUI.rotation += gradientOffsetIncrement;
            }

            // reset bouncer
            if(gradientUI.offset >= .9) { offsetBouncer = 0; }
            else if (gradientUI.offset <= -.9) { offsetBouncer = 1; }
        }
    }
}
