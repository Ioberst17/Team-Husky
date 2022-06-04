using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;
using UnityEngine.UI;

public class EXPSliderGradient : MonoBehaviour
{
    // ATTACHED TO END OF LEVEL EXP SLIDER TO MANAGE GRADIENT AND SHINE EFFECT

    public UIGradient gradientUI;
    public UIShiny shinyUI;
    public Image fillColor;
    public static float gradientRotationIncrement;
    public int rotationBouncer;
    public static float gradientOffsetIncrement;
    public int offsetBouncer;

    // Start is called before the first frame update
    void Start()
    {
        fillColor = gameObject.GetComponent<Image>();
        shinyUI = gameObject.GetComponent<UIShiny>();
        gradientUI = gameObject.GetComponent<UIGradient>();
        shinyUI.Play(); // turn on shiny animation
        shinyUI.effectPlayer.loop = true;
        gradientRotationIncrement = .02F;
        rotationBouncer = 179; // if 179, headed up to 179; if -179, headed down to -179
        gradientOffsetIncrement = .001F;
        offsetBouncer = 1; // if 1, headed up to 1; if -1, headed down to -1
    }

    // Update is called once per frame
    void Update()
    {
        Changer();
        GradientModifier();

    }

    private void FixedUpdate() // used to rotate underlying gradient
    {
        
    }

    public void Changer()
    {
        // fill should be set to white in fill slider

        //set colors of gradient
        gradientUI.color1 = new Color32(0, 82, 231, 255); // font dark blue
        gradientUI.color2 = new Color32(154, 47, 255, 255); // purple
        gradientUI.color3 = new Color32(177, 255, 241, 255); // seafoam
        gradientUI.color4 = new Color32(0, 252, 255, 255); // turquoise

        
    }
    public void GradientModifier()
    {
        {
            if (rotationBouncer == -179)
            {
                gradientUI.rotation -= gradientRotationIncrement;
            }
            else if (gradientUI.rotation == 179)
            {
                gradientUI.rotation += gradientRotationIncrement;
            }

            // reset bouncer
            if (gradientUI.rotation >= 179) { rotationBouncer = -179; }
            else if (gradientUI.rotation <= -179) { offsetBouncer = 179; }

        }
  
        {
            /* (gradientUI.offset < .9 && offsetBouncer == 1) // mod offset of gradient
            {
                gradientUI.offset += gradientOffsetIncrement;
            }
            else if (gradientUI.rotation > -.9 & offsetBouncer == -1)
            {
                gradientUI.rotation += gradientOffsetIncrement;
            }

            // reset bouncer
            if(gradientUI.offset >= .9) { offsetBouncer = -1; }
            else if (gradientUI.offset <= -.9) { offsetBouncer = 1; }*/
        }
    }
}
