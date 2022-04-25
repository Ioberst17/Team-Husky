using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //Movement
    public float speed;
    public float speedMod;

    public float rotationMod;
    public float jumpPower;
    public float jumpNumber;
    float jumpsRemaining;

    //getting references for different parts of the player entity
    private Rigidbody2D rb;
    private BoxCollider2D HurtBox;

    //Number crunching variables
    [SerializeField] private int startingHP;
    public int HealthPoints;

    //sets the ammount of invincibility frames given after a hit and tracks them
    [SerializeField] private int invincibilityValue;
    private int invincibilityTimer = 0;


    //this is for personal use in checking speed
    private float speedometer = 0;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private int count;

    public UIController UIController;

    float moveVelocity = 0;

    //Holds the state of the game from among: running, paused, levelComplete
    public string gameState;

    //Holds the state of the game from among: grounded, airborn
    public string playerState;

    public Transform GroundChecker; // circle collider located under the player object, used to check if on the ground

    //Different Layers used
    public LayerMask GroundLayer;
    public LayerMask DeathPlane;
    public LayerMask Enemies;


    [SerializeField] private Transform spawnPoint;

    //Grounded Vars
    bool grounded = true;

    //Event reporting system
    //public delegate void MyDelegate();
    //public event MyDelegate onDeath;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HurtBox = GetComponent<BoxCollider2D>();
        HealthPoints = startingHP;

        playerState = "Start";
        if (previousPosition == null)
        {
            previousPosition = rb.transform.position;
        }
        UIController.levelStart();
        count = 0;


        if (playerState == "Start")
        {
            //filler method to remove warnings for now
        }
    }


    void Update()
    {
        if (gameState != "Paused" && gameState != "levelComplete")
        {
            isGrounded();
            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
            {
                if (grounded || jumpsRemaining > 0)
                {
                    rb.AddForce(Vector2.up * jumpPower); // = new Vector2(rb.velocity.x, jumpPower);
                    rb.rotation += 15;
                    jumpsRemaining -= 1;
                }
            }

            //Controlls the base and maximum speed
            if (moveVelocity < speed)
            {
                moveVelocity += speedMod / 10;
            }
            else if (moveVelocity > speed)
            {
                moveVelocity -= speedMod / 10;
            }

            //Left Right acceleration 
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && moveVelocity > speed / 1.5)
            {
                moveVelocity = moveVelocity - speedMod;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && moveVelocity < speed * 1.5)
            {
                moveVelocity = moveVelocity + speedMod;
            }
            //setting the new speed
            rb.velocity = new Vector2(moveVelocity, rb.velocity.y);


            //control the rotation of the player in the air
            if (!grounded)
            {
                if (rb.rotation > 25)
                {
                    rb.rotation = 25;
                }
                else if (rb.rotation < -25)
                {
                    rb.rotation = -25;
                }

                if (rb.rotation > -25)
                {
                    rb.rotation -= rotationMod;
                }
            }

            //this are testing functions to be removed later.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                takeDamage(1);
            }

            checkSpeed();

            handleInvincibilityTimer();

            //manages deaths from deathplanes
            if (isDead())
            {
                Death();
            }

            
        }
    }

    //Check if Grounded
    void isGrounded()
    {
        grounded = Physics2D.OverlapCircle(GroundChecker.position, GroundChecker.GetComponent<CircleCollider2D>().radius, GroundLayer);
        if (grounded)
        {
            jumpsRemaining = jumpNumber;
            playerState = "grounded";
        }
        else
        {
            playerState = "airborn";
        }
    }
    //checks if player is dead
    bool isDead()
    {
        if (HurtBox.IsTouchingLayers(DeathPlane))
        {
            return true;
        }
        return false;
    }

    //a testing function to be removed later. Calculates speed
    void checkSpeed()
    {
        if (count > 10)
        {
            currentPosition = rb.transform.position;
            Vector2 v1 = new Vector2(previousPosition.x, previousPosition.y);
            Vector2 v2 = new Vector2(currentPosition.x, currentPosition.y);
            speedometer = Vector2.Distance(v1, v2);
            previousPosition = currentPosition;
            count = 0;
        }
        else
        {
            count += 1;
        }

    }

    //processes if the player should take damage, and if so, how much, then calculates for death. 
    public void takeDamage(int damageNumber)
    {
        if (invincibilityTimer <= 0)
        {
            HealthPoints -= damageNumber;
            if (HealthPoints <= 0)
            {
                Death();
            }
            else
            {
                invincibilityTimer = invincibilityValue;

            }
            UIController.updateHealth();
        }

    }

    private void handleInvincibilityTimer()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= 1;
        }
    }



    //reports the Death event and respawns the player
    void Death()
    {
        //onDeath.Invoke();
        Debug.Log("The Player Has died");
        rb.velocity = new Vector2(0, 0);
        rb.rotation = 0;
        rb.transform.position = spawnPoint.position;

        //resetting all values

        invincibilityTimer = 0;
        HealthPoints = startingHP;
        UIController.updateHealth();

    }
}