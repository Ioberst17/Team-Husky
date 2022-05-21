using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIceBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 startingLocation;
    private bool fallTrigger;
    private int fallTimer;
    public int fallTimerValue;
    [SerializeField] private float fallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallTrigger = false;
        fallTimer = fallTimerValue;
        startingLocation = transform.position;
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
            fallTrigger = true;
            Debug.Log("I'm falling!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (fallTrigger)
        {
            if (fallTimer <= 540)
            {
                rb.velocity = new Vector2(0, -fallSpeed/20);
            }
            if (fallTimer <= 180)
            {
                rb.velocity = new Vector2(0, -fallSpeed/10);
            }
            if (fallTimer <= 90)
            {
                rb.velocity = new Vector2(0, -fallSpeed /2);
            }
            if (fallTimer < 30)
            {
                rb.velocity = new Vector2(0, -fallSpeed);
            }
            if (fallTimer > 0)
            {
                fallTimer -= 1;
            }
        }
    }

    void OnRespawn()
    {
        transform.position = startingLocation;
        rb.velocity = new Vector2(0, 0);
        fallTimer = fallTimerValue;
        fallTrigger = false;
        Debug.Log(name + " Respawned");
    }
}
