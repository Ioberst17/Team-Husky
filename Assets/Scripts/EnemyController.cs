using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //the ammount of damage the enemy inflicts on contact
    [SerializeField] public int damageValue;

    [SerializeField] public PlayerController PlayerController;

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
