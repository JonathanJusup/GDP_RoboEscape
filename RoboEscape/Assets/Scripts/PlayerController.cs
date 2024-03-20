using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class that controls how the player moves and behaves in certain situations (i.e. dying, jumping)
 *
 * @authors Florian Kern (cgt104661), Jonathan Jusup (cgt104707), Prince Lare-Lantone (cgt104645)
 */
public class PlayerController : MonoBehaviour {
    
    /** The speed of the player */
    public float speed = 5.0f;
    
    /** The force how strong the player jumps off the ground */
    public float jumpPower = 5.0f;
    
    /** Flag that states if the player is on the ground or not */
    public bool isGrounded = true;
    
    /** Flag that states if the player is alive or not */
    public bool isAlive = true;
    
    /** The horizontal movement of the player */
    private float horizontalMovement;
    
    /** The Rigidbody component of the player */
    private Rigidbody rb;
    
    /** animator of the player */
    Animator animator;
    
    /** The soundmanager for SFX (i.e. jumping) */
    private SoundManager _soundManager;
    
    /** Spawner for individual parts of the robot model */
    private RobotPartsSpawner _robotPartsSpawner;
    
    /** Value that decides how fast the player is falling to the ground */
    public float fallMultiplier = 2.5f;
    
    /** Keeps the gravity at a lower level when the player is jumping */
    public float lowJumpMultiplier = 2.0f;

    /** Flag for deciding if a player is moving or not */
    private bool _isMoving = false;
    
    /** Flag for deciding if a player is running or not */
    private bool _isRunning = false;

    
    public bool IsMoving {
        get { return _isMoving; }
        private set {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    public bool IsRunning {
        get { return _isRunning; }
        private set {
            _isRunning = value;
            animator.SetBool("IsRunning", value);
        }
    }

    /**
     * Method is called before the first frame update.
     * Sets relevant components for the player such as the robotPartsSpawner
     */
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        _soundManager = FindObjectOfType<SoundManager>();
        _robotPartsSpawner = this.GetComponent<RobotPartsSpawner>();

        isAlive = true;
    }

    /**
     * Method is called once per frame.
     * Checks for certain states in the game and allows the player to move the model.
     */
    void Update() {
        
        // Disabling controls when game is paused
        if (PauseMenuController.IsPaused) {
            return;
        }

        // Setting the velocity to zero if player is dead and disabling controls
        if (!this.isAlive) {
            rb.velocity = Vector3.zero;
            return;
        }
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        animator.SetBool("isGrounded", isGrounded);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            Debug.Log("Raycast hit the ground at: " + hit.point + isGrounded);
            animator.SetBool("isGrounded", isGrounded);
        }
        Jump();
        Move();

        //Increase Gravity when falling, keep it lower, when high jumping
        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (rb.velocity.y > 0.0f && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }
    
    
    /**
     * Handles the horizontal movement of the player. Rotates the player model in the correct position depending on
     * in which direction the player moves.
     */
    void Move() {
        float horizontalMovement = Input.GetAxis("Horizontal");
        IsMoving = Mathf.Abs(horizontalMovement) > 0.1f;

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, 0.0f);
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0.0f);
        
        //Rotate player according to movement direction
        if (horizontalMovement < 0) {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        } else if (horizontalMovement > 0) {
            transform.rotation = transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
    
    /**
     * Handles the vertical movement of the player. Executes the jumping animation.
     */
    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            animator.SetTrigger("JumpTrigger");
            if (_soundManager)
            {
                _soundManager.PlaySound("Jump");
            }
            //Reset y-velocity before jumping
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(velocity.x, 0.0f, velocity.z);
            rb.velocity = velocity;
            
            //Execute jump
            rb.velocity = Vector3.up * jumpPower; 
            //isGrounded = false;

            
        }
    }
    
    /**
     * Handles the event in which the player dies. Executes
     */
    public void Die() {
        if (!isAlive) {
            return;
        }
        
        isAlive = false;
        animator.SetTrigger("DeathTrigger");
        
        Vector3 playerPos = transform.position;
        // Spawing individual parts of the robot
        _robotPartsSpawner.SpawnParts(playerPos);
        if (_soundManager) {
            _soundManager.PlaySound("PlayerDeath");
        }
        // Resetting the level after a delay
        StartCoroutine(ResetAfterDelay());
        

        //Deactivate all children to make them invisible
        foreach (Transform child in this.transform) {
            child.gameObject.SetActive(false);
        }
    }

    /**
     * Sets a flag to decide if the player is currently touching the floor or not.
     *
     * @param isGround Flag that decides if the player is currently on the ground or not.
     */
    public void SetIsGrounded(bool isGround) {
        isGrounded = isGround;
    }
    
    /**
     * Resets the level three seconds after the player dies.
     */
    private IEnumerator ResetAfterDelay()
    {
        Debug.Log("Reset in 3 Seconds");
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}