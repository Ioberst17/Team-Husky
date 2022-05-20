using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //the amount of damage the enemy inflicts on contact
    [SerializeField] public int damageValue;

    [SerializeField] public PlayerController PlayerController;

    public Rigidbody2D rb;
    [SerializeField] private float fallSpeed;
    [SerializeField] private bool isIcicle;
    [SerializeField] private Vector3 startingLocation;
    private bool fallTrigger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallTrigger = !isIcicle;
        startingLocation = transform.position;
        PlayerController.onDeath += OnRespawn;
        Debug.Log(name + " starting location is " + startingLocation);
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
                PlayerController.takeDamage(damageValue);
            }
        }
    }
    void OnRespawn()
    {
        transform.position = startingLocation;
        rb.velocity = new Vector2(0, 0);
        fallTrigger = !isIcicle;
        Debug.Log(name + " Respawned");
    }
}
