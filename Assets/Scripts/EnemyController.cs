using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //the amount of damage the enemy inflicts on contact
    [SerializeField] public int damageValue;

    [SerializeField] public PlayerController PlayerController;

    private Rigidbody2D rb;
    private SpriteRenderer EnemySpriteRenderer;
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
        EnemySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        PlayerController.onDeath += OnRespawn;
    }
    private void OnDisable()
    {
        PlayerController.onDeath -= OnRespawn;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
        {
            if (!fallTrigger)
            {
                fallTrigger = true;
                rb.velocity = new Vector2(0, -fallSpeed);
            }
            else
            {
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
    void OnRespawn()
    {
        transform.position = startingLocation;
        rb.velocity = new Vector2(0, 0);
        fallTrigger = !isIcicle;
        EnemySpriteRenderer.enabled = true;


        //Debug.Log(name + " Respawned");
    }
}
