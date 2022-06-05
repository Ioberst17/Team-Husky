using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseSensorBehavior : MonoBehaviour
{
    public PlayerController PlayerController;
    public bool isgrounded;
    public int count;
    [SerializeField] private int countPoint;
    [SerializeField] private float bounceForce;
    [SerializeField] private float torqueForce;
    [SerializeField] private int Lockoutvalue;

    private void Start()
    {
        isgrounded = false;
        count = 0;
    }
    private void FixedUpdate()
    {
        if (isgrounded)
        {
            count += 1;
        }
        else if(count > 0)
        {
            count -= 1;
        }
        if(count >= countPoint)
        {
            count = 0;
            PlayerController.rb.AddTorque(-torqueForce);
            PlayerController.ControlLockout = Lockoutvalue;
            PlayerController.rb.AddForce(Vector2.left * bounceForce);
            PlayerController.moveVelocity = 0;
        }
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
