using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelController : MonoBehaviour
{
    public PlayerController PlayerController;
    public UIController UIController;
    bool triggered;

    void Awake()
    {
        triggered = false;
    }
    // called whenever another collider enters our zone (if layers match)
    void OnTriggerEnter2D(Collider2D collider)
    {
        // check we haven't been triggered yet!
        if (!triggered)
        {

            if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
            {
                Trigger();
            }
        }
    }
    void Trigger()
    {
        PlayerController.levelComplete = true;
        triggered = true;
        Debug.Log("Level Over!");
    }

}
