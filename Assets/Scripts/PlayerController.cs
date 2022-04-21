using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //Movement
    public float speed;
    public float speedMod;
    private float speedometer = 0;
    public float rotationMod;
    public float jumpPower;
    public float jumpNumber;

    [SerializeField] private int startingHP;
    private int HealthPoints;

    float jumpsRemaining;
    private Rigidbody2D rb;

    //this is for personal use in checking speed
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private int count;


    float moveVelocity = 0;
    private string state;

    public Transform GroundChecker; // circle collider located under the player object, used to check if on the ground
    public LayerMask GroundLayer;
    public LayerMask DeathPlane;

    [SerializeField] private Transform spawnPoint;
    

    //Grounded Vars
    bool grounded = true;

    //Event reporting system
    //public delegate void MyDelegate();
    //public event MyDelegate onDeath;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HealthPoints = startingHP;
        state = "start";
        if (previousPosition == null)
        {
            previousPosition = rb.transform.position;
        }
        count = 0;
        if(state == "start")
        {
            //filler method to remove warnings for now
        }
    }
        void Update()
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
        checkSpeed();
        if (isDead())
        {
            Death();
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
    //checks if player is dead
    bool isDead()
    {
        if (Physics2D.OverlapCircle(GroundChecker.position, GroundChecker.GetComponent<CircleCollider2D>().radius, DeathPlane))
            {
            return true;
            }
        return false;
    }

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
    
    //reports the Death event
    void Death()
    {
        //onDeath.Invoke();
        Debug.Log("The Player Has died");
        rb.velocity = new Vector2(0, 0);
        rb.rotation = 0;
        rb.transform.position = spawnPoint.position;

        HealthPoints = startingHP;

    }
}