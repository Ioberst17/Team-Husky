using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerScript : MonoBehaviour
{
    public PlayerController PlayerController;
    public bool isgrounded;


    private void Start()
    {
        isgrounded = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if its the ground
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Environment"))
        {
            isgrounded = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Environment"))
        {
            isgrounded = true;
        }
    }
            private void OnTriggerExit2D(Collider2D collider)
    {
        //if its the ground
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Environment"))
        {
            isgrounded = false;
        }
    }


}
