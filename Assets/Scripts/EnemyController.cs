using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //the amount of damage the enemy inflicts on contact
    [SerializeField] public int damageValue;

    //this is required to communicate fully with the player
    [SerializeField] public PlayerController PlayerController;


    private Rigidbody2D rb;
    [SerializeField] private float fallSpeed;
    [SerializeField] private bool isIcicle;
    [SerializeField] private bool isHazard;
    private Vector3 startingLocation;
    private bool fallTrigger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallTrigger = !isIcicle;
        startingLocation = transform.position;
        PlayerController.onDeath += OnRespawn;
    }
    //make sure to remove itself when the level restarts
    private void OnDisable()
    {
        PlayerController.onDeath -= OnRespawn;
    }
    //when something enters the zone
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if its a player
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
        {
            //are we an icicle?
            if (!fallTrigger)
            {
                fallTrigger = true;
                rb.velocity = new Vector2(0, -fallSpeed);
            }
            else
            {
                //are we a hazard?
                if (isHazard)
                {
                    //do nothing, the ontriggerstay function will handle it
                }
                else
                {
                    PlayerController.takeDamage(damageValue, 0);
                }
                
            }
        }
    }
    //deals faster, lighter damage
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
        {
            if (isHazard)
            {
                PlayerController.takeDamage(damageValue, 1);

            }
        }
    }
    //reset everything on respawn
    void OnRespawn()
    {
        transform.position = startingLocation;
        rb.velocity = new Vector2(0, 0);
        fallTrigger = !isIcicle;
        //Debug.Log(name + " Respawned");
    }
}
