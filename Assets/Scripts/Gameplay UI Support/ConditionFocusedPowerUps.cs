using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionFocusedPowerUps : MonoBehaviour
{
    // manages the scaling up and down of Condition Focused Powerups that run over a period of time: Invincibility and Golden (Skates)

    public Animator animator;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.invincibilityOn) // INVINCIBILITY CHECKER
        {
            if (gameObject.name == "Slider") // check for the slider
            {
                animator.SetInteger("SliderLarge", 1); // if so set the slider to animate
            }

            else
            {
                if (gameObject.transform.parent.gameObject.name == "Invincibility") // check if the parent to the image is Invincibility
                {
                    animator.SetInteger("ButtonLarge", 1); // turn on anim
                }
            }
        }
        else if (playerController.goldenOn) //GOLDEN CONDITION CHECKER
        {
            if (gameObject.transform.parent.gameObject.name == "Golden") // check if the parent to the image is Golden
            {
                animator.SetInteger("ButtonLarge", 1); // turn on anim
            }
        }
        else // turn off animations
        {
            if (gameObject.name == "Slider")
            {
                animator.SetInteger("SliderLarge", 0); // turn off anim
            }

            else
            {
                animator.SetInteger("ButtonLarge", 0); // turn off anim
            }
        }
    }
}

