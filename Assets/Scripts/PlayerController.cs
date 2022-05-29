using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Movement
    [SerializeField] private float speed;
    [SerializeField] private float speedMod;
    [SerializeField] private float rotationMod;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpNumber;
    float jumpsRemaining;
    float moveVelocity = 0;
    public ParticleSystem movementDustParticles;

    //getting references for different parts of the player entity
    public Rigidbody2D rb;
    private BoxCollider2D HurtBox;
    private Animator animator;

    //Number crunching variables
    [SerializeField] private int startingHP;
    [SerializeField] private int landingLag;
    [SerializeField] private float maxSpeedMult;
    [SerializeField] private float airControlMod;
    public int HPSliderMax;
    public int HealthPoints;

    //sets the ammount of invincibility frames given after a hit and tracks them
    [SerializeField] private int invincibilityValue;
    private int invincibilityTimer = 0;


    //this is for personal use in checking speed
    public float speedometer = 0;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private int count;
    public int stuckCount;

    //various different controllers from around the scene.
    public MusicController MusicController;
    public UIController UIController;

    public bool levelComplete;

    //Holds the state of the game from among: running, paused
    public string gameState;

    //Holds the state of the game from among: grounded, airborn
    public string playerState;

    public Transform GroundChecker; // circle collider located under the player object, used to check if on the ground
    public Transform GroundChecker2;
    public Transform GroundChecker3;

    //Different Layers used
    public LayerMask GroundLayer;
    public LayerMask DeathPlane;
    public LayerMask Enemies;


    [SerializeField] private Transform spawnPoint;


    //variables for transmitting user input into fixed update
    private bool jumpInput;
    private int accelerationInput;
    private int powerupInput;

    //Verious timer values
    private int jumpTimer;
    private int readySetGoTimer;
    private int landingTimer;
    public Stopwatch Stopwatch;

    //Grounded Vars
    bool grounded = true;
    bool grounded2 = true;
    bool grounded3= true;
    bool groundedUnified = true;

    //Inventory
    public Inventory inventory;

    //Mushing related
    public bool canMush = true; // need public for UI events
    [SerializeField] private float mushingCooldown;
    [SerializeField] private float mushForce;
    public ParticleSystem mushUse;
    Animator mushUIButtonAnimation;

    //Invincibility powerup related
    [SerializeField] private float invincibilityLength;
    private float invincibilityCounter = 0;
    public bool invincibilityOn = false; // need public for UI events
    public ParticleSystem invincibilityUse;

    //golden powerup related
    [SerializeField] private float goldenLength;
    private float goldenCounter = 0;
    public bool goldenOn = false; // need public for UI events
    public ParticleSystem golden1Use;
    public ParticleSystem golden2Use;


    //Toolkit related
    public ParticleSystem toolkitUse;
    Animator toolkitUIButtonAnimation;

    //ReadySetGoParticles
    public ParticleSystem readySetGoParticle1A; // assigned in inspector
    public ParticleSystem readySetGoParticle2A;
    public ParticleSystem readySetGoParticle3A;
    public ParticleSystem readySetGoParticle4A;
    public ParticleSystem readySetGoParticle1B; 
    public ParticleSystem readySetGoParticle2B;
    public ParticleSystem readySetGoParticle3B;
    public ParticleSystem readySetGoParticle4B;
    public bool readySetGoParticleFadeOut = false;


    //Event reporting system
    public delegate void MyDelegate();
    public static event MyDelegate onDeath;

    public GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HurtBox = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        inventory = GetComponentInParent<Inventory>();
        Stopwatch = FindObjectOfType<Stopwatch>();
        mushUIButtonAnimation = GameObject.Find("Musher").transform.Find("Image").GetComponent<Animator>();
        toolkitUIButtonAnimation = GameObject.Find("Toolkit").transform.Find("Image").GetComponent<Animator>();
        gameManager = GameManager.Instance;
        HPSliderMax = startingHP;
        HealthPoints = startingHP;

        readySetGoParticle1B.Stop();
        readySetGoParticle2B.Stop();
        readySetGoParticle3B.Stop();
        readySetGoParticle4B.Stop();


        playerState = "Start";
        gameState = "Starting";
        //SceneDataLoader();
        levelComplete = false;
        if (previousPosition == null)
        {
            previousPosition = rb.transform.position;
        }
        UIController.levelStart();
        MusicController.levelStart();
        count = 0;
        stuckCount = 0;
        jumpTimer = 0;
        landingTimer = 0;
        readySetGoTimer = 0;
        powerupInput = 0; ;
        readySetGo();

    }

    //update is largely focused on user input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            pauseHandler();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote) && gameState == "paused")
        {
            Debug.Log("debugMenu");
            UIController.toggleDebugMenu();
        }

        if (gameState != "Paused" && !levelComplete && gameState != "Starting")
        {
            //Jumping
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W))
            {
                jumpInput = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
            {
                jumpInput = true;
            }
            
            //Left Right acceleration 
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
            {
                
                accelerationInput = -1;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
            {
                
                accelerationInput = 1;
            }
            else
            {
                accelerationInput = 0;
            }

            //this is a testing functions to be removed later.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                takeDamage(50, 0);
            }

            // Powerup-related

            if (Input.GetKeyDown(KeyCode.F) && canMush == true && inventory.characterItems[0].amount > 0) // for mushing
            {
                powerupInput = 1;
            }

            if (Input.GetKeyDown(KeyCode.G) && invincibilityOn == false && inventory.characterItems[1].amount > 0) // for invincibility
            {
                powerupInput = 2;
            }

            if (Input.GetKeyDown(KeyCode.B) && goldenOn == false && inventory.characterItems[2].amount > 0) // for golden
            { 
                powerupInput = 3;
            }

            if (Input.GetKeyDown(KeyCode.V) && inventory.characterItems[3].amount > 0) // for toolkit
            {
                powerupInput = 4;
            }
        }

        if(rb.velocity.x > 1) // turn off player highlighter particles if not ready, set, go
        {
            ReadySetGoParticleFade();
        }

        if (levelComplete)
        {
            accelerationInput = -1;
            jumpInput = false;

            // save session data
            gameManager.EndSceneDataSaver(
                gameManager.seshData,
                HealthPoints,
                inventory.characterItems[0].amount,
                inventory.characterItems[1].amount,
                inventory.characterItems[2].amount,
                inventory.characterItems[3].amount);

            // check for new high score
            gameManager.CheckForNewHighScore(gameManager.gameData, gameManager.seshData, 1, Stopwatch.GetRawElapsedTime());

            // solves for delay between timer stopping and checkForNewHighScore firing
            UIController.timerText.text = string.Format("{0:0}:{1:00}:{2:00}",
                                                        Stopwatch.GetMinutes(),
                                                        Stopwatch.GetSeconds() - 60 * Stopwatch.GetMinutes(),
                                                        (Stopwatch.GetMilliseconds() * 100.00f) % 100.00f);
        }
    }

    //Fixed update is where all physics happens
    void FixedUpdate()
    {
        readySetGo();
        isGrounded();
        if (canMush)
        {
            //Controlls the base and maximum speed if not using a powerup
            if (moveVelocity < 0)
            {
                moveVelocity = 0;
            }
            else if (moveVelocity > speed)
            {
                moveVelocity -= speedMod / 10;
            }
            Accelerate(accelerationInput);
        }
        

        Jump(jumpInput);

        

        UsePowerup();

        //control the rotation of the player    
        if(rb.rotation > 55)
        {
            rb.rotation = 55;
        }
        if (rb.rotation < -55)
        {
            rb.rotation = -55;
        }
        if (!grounded && !grounded2 && !grounded3)
        {
            rb.freezeRotation = true;
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

        handleInvincibilityTimer();

        //manages deaths from deathplanes
        if (isOutOfBounds())
        {
            Death();
        }
    }

    //handles acceleration inputs
    void Accelerate(int accelInput)
    {
        if ((groundedUnified) && jumpTimer == 0)
        {
            //if holding back and with positive velocity, slow down
            if (accelInput == -1 && moveVelocity > 0)
            {
                moveVelocity = moveVelocity - speedMod;
                if (moveVelocity < 0)
                {
                    moveVelocity = 0;
                }
            }
            //if holding forward and not exceeding the maximum, speed up
            if (accelInput == 1 && moveVelocity < speed * maxSpeedMult)
            {
                moveVelocity = moveVelocity + speedMod;
            }
            //handles the animation states
            if(landingTimer == 0 && invincibilityTimer == 0)
            {
                if (moveVelocity < 1)
                {
                    animator.Play("PlayerRest");
                }
                else
                {
                    animator.Play("PlayerRunning");
                    CreateMovementDust();
                }
            }
            rb.AddForce(Vector2.down * 50);
            
        }
        else
        {
            //Air control is reduced
            if (accelInput == -1 && moveVelocity > 0)
            {
                moveVelocity = moveVelocity - speedMod/airControlMod;
                if (moveVelocity < 0)
                {
                    moveVelocity = 0;
                }
            }
            if (accelInput == 1 && moveVelocity < speed * maxSpeedMult)
            {
                moveVelocity = moveVelocity + speedMod/airControlMod;
            }
        }

        //setting the new speed
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
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
            if(speedometer <= 0.5f && !groundedUnified)
            {
                stuckCount += 1;
            }
            else
            {
                stuckCount = 0;
            }
            if(stuckCount >= 1)
            {
                stuckCount = 0;
                rb.velocity = new Vector2(-1, -4);
                moveVelocity = 0;
            }
        }
        else
        {
            count += 1;
        }

    }

    //reports the Death event and respawns the player
    void Death()
    {
        onDeath?.Invoke();
        Debug.Log("The Player Has died");
        rb.velocity = new Vector2(0, 0);
        rb.rotation = 0;
        rb.transform.position = spawnPoint.position;

        //resetting all values
        animator.Play("PlayerRest");
        moveVelocity = 0;
        accelerationInput = 0;
        jumpInput = false;
        invincibilityTimer = 0;
        jumpTimer = 0;
        landingTimer = 0;
        HealthPoints = startingHP;
        UIController.updateHealth();

    }

    private void handleInvincibilityTimer()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= 1;
        }
    }

    //Check if Grounded
    void isGrounded()
    {
        bool isLanding = false;
        if (playerState == "airborn")
        {
            isLanding = true;
        }
        grounded = Physics2D.OverlapCircle(GroundChecker.position, GroundChecker.GetComponent<CircleCollider2D>().radius, GroundLayer);
        grounded2 = Physics2D.OverlapCircle(GroundChecker2.position, GroundChecker2.GetComponent<CircleCollider2D>().radius, GroundLayer);
        grounded3 = Physics2D.OverlapCircle(GroundChecker3.position, GroundChecker3.GetComponent<CircleCollider2D>().radius, GroundLayer);
        groundedUnified = (grounded && grounded2 && grounded3);
        if (grounded || grounded2 || grounded3)
        {
            rb.freezeRotation = false;
        }
        if (groundedUnified) 
        {
            jumpsRemaining = jumpNumber;
            playerState = "grounded";
            
            if (landingTimer > 0)
            {
                landingTimer -= 1;
            }
            
            if (isLanding)
            {
                landingTimer = landingLag;
                animator.Play("PlayerLand");
                CreateMovementDust();
            }
        }
        else
        {
            playerState = "airborn";
            StopMovementDust();
        }
    }



    //checks if player is dead
    bool isOutOfBounds()
    {
        if (HurtBox.IsTouchingLayers(DeathPlane))
        {
            MusicController.outOfBoundsFunction();
            return true;
        }
        return false;
    }

    //handles jumping
    void Jump(bool jInput)
    {
        if (jInput)
        {
            if (((groundedUnified) || jumpsRemaining > 0) && jumpTimer == 0 && landingTimer == 0)
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector2(moveVelocity, 0);
                }
                MusicController.JumpFunction();
                animator.Play("PlayerJump");
                CreateMovementDust();
                rb.AddForce(Vector2.up * jumpPower); 
                rb.rotation = 25;
                jumpsRemaining -= 1;
                jumpTimer = 15;
                jumpInput = false;
            }
        }
        if (jumpTimer > 0)
        {
            jumpTimer -= 1;
        }
    }

    public void pauseHandler()
    {
        if (gameState != "paused")
        {
            Time.timeScale = 0;
            gameState = "paused";
            MusicController.AudioSource.Pause();

        }
        else
        {
            Time.timeScale = 1;
            gameState = "running";
            MusicController.AudioSource.UnPause();
        }

    }
    
    private void readySetGo()
    {
        if (readySetGoTimer <= 180)
        {
            if (readySetGoTimer == 0)
            {
                MusicController.ReadySetGoFunction();
                UIController.readySetGoText.text = "Ready";
            }
            else if (readySetGoTimer == 60)
            {
                UIController.readySetGoText.text = "Set";
            }
            else if (readySetGoTimer == 120)
            {
                UIController.readySetGoText.text = "Go!";
                gameState = "running";
                UIController.timerStart();
            }
            else if (readySetGoTimer == 180)
            {
                UIController.readySetGoText.text = "";
            }
            readySetGoTimer += 1;
        }
    }
    
    //processes if the player should take damage, and if so, how much, then calculates for death. damageType Numbers: 0 is one hit damage, 1 is damage over time.
    public void takeDamage(int damageNumber, int damageType)
    {
        if (invincibilityTimer <= 0 && invincibilityOn == false)
        {
            switch (damageType)
            {
                case 0:
                    HealthPoints -= damageNumber;
                    if (HealthPoints <= 0)
                    {
                        Death();
                        break;
                    }
                    MusicController.DamageFunction();
                    invincibilityTimer = invincibilityValue;
                    if (groundedUnified)
                    {
                        animator.Play("PlayerRunningDamage_Ian");
                    }
                    else
                    {
                        animator.Play("PlayerJumpDamage_Ian");
                    }
                    break;
                case 1:
                    if(speedometer >= 1)
                    {
                        HealthPoints -= damageNumber;
                        if (HealthPoints <= 0)
                        {
                            Death();
                            break;
                        }
                        MusicController.DamageFunction();
                        invincibilityTimer = invincibilityValue / 2;
                        if (groundedUnified)
                        {
                            animator.Play("PlayerRunningDamage_Ian");
                        }
                        else
                        {
                            animator.Play("PlayerJumpDamage_Ian");
                        }
                    }
                    break;
            }
        }
    UIController.updateHealth();
    }
    
    public void ReadySetGoParticleFade() // controls the fade of the ReadySetGo Particles after the player starts moving
    {
        if(readySetGoParticleFadeOut == false)
        {
            readySetGoParticle1A.gameObject.SetActive(false);
            readySetGoParticle2A.gameObject.SetActive(false);
            readySetGoParticle3A.gameObject.SetActive(false);
            readySetGoParticle4A.gameObject.SetActive(false);

            readySetGoParticle1B.gameObject.SetActive(true);
            readySetGoParticle2B.gameObject.SetActive(true);
            readySetGoParticle3B.gameObject.SetActive(true);
            readySetGoParticle4B.gameObject.SetActive(true);

            readySetGoParticle1B.Play();
            readySetGoParticle2B.Play();
            readySetGoParticle3B.Play();
            readySetGoParticle4B.Play();

            readySetGoParticleFadeOut = true;
        }
        else
        {
            readySetGoParticle1B.Stop();
            readySetGoParticle2B.Stop();
            readySetGoParticle3B.Stop();
            readySetGoParticle4B.Stop();
        }
    }

    void CreateMovementDust()
    {
        movementDustParticles.Play();
    }

    void StopMovementDust()
    {
        movementDustParticles.Stop();
    }

    void UsePowerup()
    {
        switch (powerupInput)
        {
            case 0:
                break;

            case 1:
                MusicController.SpeedBoostFunction();
                rb.AddForce(transform.right * mushForce, ForceMode2D.Impulse);
                mushUse.Play();
                canMush = false;
                mushUIButtonAnimation.SetTrigger("TriggerPowerUpScale");
                StartCoroutine(MushingRoutine());
                inventory.RemoveItem(0);
                powerupInput = 0;
                break;
            case 2: //invincibility
                invincibilityOn = true;
                invincibilityUse.Play();
                StartCoroutine(InvincibilityRoutine());
                invincibilityCounter = 0;
                inventory.RemoveItem(1);
                powerupInput = 0;
                break;
            case 3: //golden
                goldenOn = true;
                golden1Use.Stop();
                golden2Use.Stop();
                golden1Use.Play();
                golden2Use.Play();
                StartCoroutine(GoldenRoutine());
                goldenCounter = 0;
                inventory.RemoveItem(2);
                powerupInput = 0;
                break;
            case 4: //toolkit
                HealthPoints += 10;
                toolkitUse.Play();
                inventory.RemoveItem(3);
                toolkitUIButtonAnimation.SetTrigger("TriggerPowerUpScale");
                UIController.updateHealth();
                powerupInput = 0;
                break;
        }
    }

    IEnumerator MushingRoutine() // is called by the trigger event for powerups to countdown how long the power lasts
    {
        yield return new WaitForSeconds(mushingCooldown); // waits a certain number of seconds
        canMush = true;
    }

    IEnumerator InvincibilityRoutine() // is called by the trigger event for powerupts to countdown how long 
    {
        while (invincibilityCounter < invincibilityLength)
        {
            yield return new WaitWhile(() => gameState == "paused");

            invincibilityCounter += Time.deltaTime;
            yield return null;
        }
        invincibilityOn = false;
        invincibilityUse.Stop();
        yield return null;
    } 

    IEnumerator GoldenRoutine()
    {
        while (goldenCounter < goldenLength)
        {
            yield return new WaitWhile(() => gameState == "paused");

            goldenCounter += Time.deltaTime;
            yield return null;
        }
        goldenOn = false;
        golden1Use.Stop();
        golden2Use.Stop();
        yield return null;
    }

    public void SceneDataLoader()
    {
        if (gameManager.sceneHistory[gameManager.sceneHistory.Count - 1] == 0) //if the previous screen was the main menu
        {
            // do nothing except open up with default level values
        }
        else // load the previous values
        {
            HealthPoints = gameManager.seshData.hitPoints;
            inventory.characterItems[0].amount = gameManager.seshData.musherAmount;
            inventory.characterItems[1].amount = gameManager.seshData.invincibilityAmount;
            inventory.characterItems[2].amount = gameManager.seshData.goldenAmount;
            inventory.characterItems[3].amount = gameManager.seshData.toolkitAmount;
        }

        // UI updates
        UIController.updateHealth(); // update health UI
        for (int i = 0; i < 4; i++) // update inventory UI
        {
            inventory.updateUI(i);
        }
    }
}