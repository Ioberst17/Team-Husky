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
    private Rigidbody2D rb;
    float moveVelocity = 0;
    public string state;

    public Transform GroundChecker; // circle collider located under the player object, used to check if on the ground
    public LayerMask GroundLayer;

    //Grounded Vars
    bool grounded = true;

    //Generic powerup-related
    public bool hasPowerup;
    private float powerupTime = 10.0f;
    public PowerUpType currentPowerUp = PowerUpType.None; // used to determine which logic to enable for the player when a powerup is collected
    // public GameObject powerupIndicator; // placeholder for animation asset



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = "start";
    }
    void Update()
    {
        isGrounded();
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded || jumpsRemaining > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                rb.rotation = 15;
                jumpsRemaining -= 1;
            }
        }

        if (moveVelocity < speed)
        {
            moveVelocity += speedMod / 10;
        }else if (moveVelocity > speed)
        {
            moveVelocity -= speedMod / 10;
        }

        //Left Right Movement
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && moveVelocity > speed/1.5)
        {
            moveVelocity = moveVelocity - speedMod;
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && moveVelocity < speed*1.5)
        {
            moveVelocity = moveVelocity + speedMod;
        }

        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

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

        // Powerup-related

        if (Input.GetKeyDown(KeyCode.R)) // for invinicibility
        {
            
            Invincibility();
        }
    }
    //Check if Grounded
    void isGrounded()
    {
        grounded = Physics2D.OverlapCircle(GroundChecker.position, GroundChecker.GetComponent<CircleCollider2D>().radius, GroundLayer);
        if (grounded)
        {
            jumpsRemaining = jumpNumber;
            state = "grounded";
        }
        else
        {
            state = "airborn";
        }
    }

    void Invincibility()
    {

    }

    IEnumerator PowerupCountdownRoutine() // is called by the trigger event for powerups to countdown how long the power lasts
    {
        yield return new WaitForSeconds(powerupTime); // waits a certain number of seconds

        // sets relevants Powerup conditions (e.g. visual indicator and bool for logic) back to 0
        currentPowerUp = PowerUpType.None; // sets powerup type back to None vs. an active one like pushback or rockets
        hasPowerup = false;
    }
}