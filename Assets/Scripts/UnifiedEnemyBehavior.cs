using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifiedEnemyBehavior : MonoBehaviour
{
    private SpriteRenderer obstacleSpriteRenderer;
    private BoxCollider2D obstacleBoxCollider; // used if obstacle has box collider
    private CircleCollider2D obstacleCircleCollider; // used if obstacle has box collider
    private PolygonCollider2D obstaclePolygonCollider; // used if obstacle has box collider
    private PlayerController playerController;
    private MusicController MusicController;
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

        // get the right collider component depending on the obstacle
        //if (gameObject.GetComponent<BoxCollider2D>() != null) { obstacleBoxCollider = gameObject.GetComponent<BoxCollider2D>(); }
        //if (gameObject.GetComponent<CircleCollider2D>() != null) { obstacleCircleCollider = gameObject.GetComponent<CircleCollider2D>(); }
        //if (gameObject.GetComponent<PolygonCollider2D>() != null) { obstaclePolygonCollider = gameObject.GetComponent<PolygonCollider2D>(); }

        //obstacleBreakSound = gameObject.GetComponent<AudioSource>(); // need to add a AudioSource component + audio clip in inspector
    }
    
    private void OnDisable()
    {
        PlayerController.onDeath -= OnRespawn;
    }

    void OnRespawn()
    {
        transform.position = startingLocation;
        rb.velocity = new Vector2(0, 0);
        fallTrigger = isIcicle;
        if (!isHazard)
        {
            obstacleSpriteRenderer.enabled = true;
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
                if (isHazard){
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
                playerController.takeDamage(damageValue, 1);

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
                break;
            case 2:
                var bolPart = boulderParticles.main;
                bolPart.startSpeed = playerController.rb.velocity.magnitude;
                snowPileParticles.Play();
                boulderParticles.Play();
                break;
            case 3:
                invincPart = invincibilityObstacleParticles.main; //note you need to instantiate particle systems modules to access underlying variables in code
                invincPart.startSpeed = playerController.rb.velocity.magnitude; // set particle system launch speed to velocity mag of player
                invincibilityObstacleParticles.Play();
                break;
        }
        obstacleID = temp;
        //disable the right box collider (if it's there) - need to disable box collider to prevent double collisions
        //if (obstacleBoxCollider != null) { obstacleBoxCollider.enabled = false; }
        //if (obstacleCircleCollider != null) { obstacleCircleCollider.enabled = false; }
        //if (obstaclePolygonCollider != null) { obstaclePolygonCollider.enabled = false; }

        //yield return new WaitForSeconds(3F);
        //if (obstacleBoxCollider != null) { obstacleBoxCollider.enabled = true; }
        //if (obstacleCircleCollider != null) { obstacleCircleCollider.enabled = true; }
        //if (obstaclePolygonCollider != null) { obstaclePolygonCollider.enabled = true; }
        //gameObject.SetActive(false); // remove the obstacle
        yield return null;
    }


    
}
