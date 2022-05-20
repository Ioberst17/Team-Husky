using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleController_Ian : MonoBehaviour
{
    //the amount of damage the enemy inflicts on contact
    [SerializeField] public int damageValue;

    [SerializeField] public PlayerController PlayerController;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
        {
            PlayerController.takeDamage(this.damageValue);
        }
    }
}
