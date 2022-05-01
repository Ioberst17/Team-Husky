using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //Movement
    private float speed = 15;
    private float speedMod = 0.1f;
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

    //Inventory
    public Inventory inventory;

    //Mushing related
    private bool canMush = true;
    private float mushingCooldown = 1.0f;
    private float mushForce = 70;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponentInParent<Inventory>();
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


        if (canMush == true) // speed modifiers should only apply while mush isn't effect
        { 
            if (moveVelocity < speed)  
            {
                moveVelocity += speedMod / 10;
            }
            else if (moveVelocity > speed) 
            {
                moveVelocity -= speedMod / 10;
            }

            //Left Right Movement
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && moveVelocity > speed / 1.5)
            {
                moveVelocity = moveVelocity - speedMod;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && moveVelocity < speed * 1.5)
            {
                moveVelocity = moveVelocity + speedMod;
            }

            rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
        }

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

        if (Input.GetKeyDown(KeyCode.F) && canMush == true && inventory.characterItems[0].amount > 1) // for mushing
        {
            rb.AddForce(transform.right * mushForce, ForceMode2D.Impulse);
            canMush = false;
            StartCoroutine(MushingRoutine());
            inventory.RemoveItem(0);
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
        // to do later
    }

    IEnumerator MushingRoutine() // is called by the trigger event for powerups to countdown how long the power lasts
    {
        yield return new WaitForSeconds(mushingCooldown); // waits a certain number of seconds
        canMush = true;
    }
}