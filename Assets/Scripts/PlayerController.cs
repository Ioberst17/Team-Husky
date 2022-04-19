using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //Movement
    public float speed;
    public float speedMod;
    public float speedometer = 0;
    public float rotationMod;
    public float jumpPower;
    public float jumpNumber;
    float jumpsRemaining;
    private Rigidbody2D rb;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private int count;
    float moveVelocity = 0;
    public string state;

    public Transform GroundChecker; // circle collider located under the player object, used to check if on the ground
    public LayerMask GroundLayer;

    //Grounded Vars
    bool grounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = "start";
        if (previousPosition == null)
        {
            previousPosition = rb.transform.position;
        }
        count = 0;
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
}