using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer obstacleSpriteRenderer;
    private PlayerController playerController;
    [SerializeField] private MusicController MusicController;
    // used to trigger particle effects on player
    private ParticleSystem invincibilityObstacleParticles;
    private ParticleSystem snowPileParticles;
    private ParticleSystem boulderParticles;
    private int obstacleID;

    //the amount of damage the enemy inflicts on contact
    [SerializeField] public int damageValue;

    //[SerializeField] public PlayerController PlayerController;
    private Rigidbody2D rb;
    [SerializeField] private float fallSpeed;
    [SerializeField] private bool isIcicle;
    [SerializeField] private bool isHazard;
    private Vector3 startingLocation;
    public bool fallTrigger;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fallTrigger = isIcicle;
        startingLocation = transform.position;
        obstacleSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        playerController = GameObject.Find("PlayerModel").GetComponent<PlayerController>(); // need for collisions
        

        invincibilityObstacleParticles = GameObject.Find("InvincibilityObstacleParticles").GetComponent<ParticleSystem>(); // need for particle effects
        snowPileParticles = GameObject.Find("SnowPileParticles").GetComponent<ParticleSystem>(); // need for particle effects
        boulderParticles = GameObject.Find("BoulderParticles").GetComponent<ParticleSystem>(); // need for particle effects

        PlayerController.onDeath += OnRespawn;
        
        //used to trigger particle systems
        if (CompareTag("SnowPile")) { obstacleID = 1; }
        if (CompareTag("Boulder")) { obstacleID = 2; }
        if (CompareTag("Icicle")) { obstacleID = 3; }
        if (CompareTag("RoughTerrain")) { obstacleID = 4; }
        if (CompareTag("BoulderFall")) { obstacleID = 5; }

       
    }

    private void OnDisable()
    {
        PlayerController.onDeath -= OnRespawn;
    }

    void OnRespawn()
    {
        transform.position = startingLocation;
        fallTrigger = isIcicle;
        if (!isHazard)
        {
            obstacleSpriteRenderer.enabled = true;
            rb.velocity = new Vector2(0, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
        {
            if (fallTrigger)
            {
                fallTrigger = false;
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
                    playerController.takeDamage(damageValue, 0);
                    StartCoroutine(Break(collider));
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
                if (!playerController.goldenOn || !playerController.invincibilityOn)
                {
                    playerController.takeDamage(damageValue, 1);
                    MusicController.hazardDamage();
                }
                

            }
        }
    }

    private IEnumerator Break(Collider2D other) // coroutine used because there needs to be a delay between object destruction (i.e. disappearance from screen) to enable particle system playing
    {
        //obstacleBreakSound.Play(); // play sound
        obstacleSpriteRenderer.enabled = false; // disable sprite
        // play a specific breakable object particle system on playerController based on current object type
        int temp = obstacleID;
        if (obstacleID == 5) { obstacleID = 2; }
        if (playerController.invincibilityOn) { obstacleID = 0; } // if the character has invincibility make this adjustment
        switch (obstacleID)
        {
            case 0:
                var invincPart = invincibilityObstacleParticles.main; //note you need to instantiate particle systems modules to access underlying variables in code
                invincPart.startSpeed = playerController.rb.velocity.magnitude; // set particle system launch speed to velocity mag of player
                invincibilityObstacleParticles.Play();
                break;
            case 1:
                var snowPart = snowPileParticles.main;
                snowPart.startSpeed = playerController.rb.velocity.magnitude;
                snowPileParticles.Play();
                MusicController.snowDamage();
                break;
            case 2:
                var bolPart = boulderParticles.main;
                bolPart.startSpeed = playerController.rb.velocity.magnitude;
                snowPileParticles.Play();
                boulderParticles.Play();
                MusicController.rockDamage();
                break;
            case 3:
                invincPart = invincibilityObstacleParticles.main; //note you need to instantiate particle systems modules to access underlying variables in code
                invincPart.startSpeed = playerController.rb.velocity.magnitude; // set particle system launch speed to velocity mag of player
                invincibilityObstacleParticles.Play();
                MusicController.iceDamage();
                break;
        }
        obstacleID = temp;
        yield return null;
    }



}

