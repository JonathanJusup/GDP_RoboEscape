using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that controls how the player moves and behaves in certain situations (i.e. dying, jumping).
///
/// @authors Florian Kern (cgt104661), Jonathan Jusup (cgt104707), Prince Lare-Lantone (cgt104645)
/// </summary>
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

    /** Capsule Collider of player */
    private CapsuleCollider _collider;

    /** Value that decides how fast the player is falling to the ground */
    public float fallMultiplier = 2.5f;

    /** Keeps the gravity at a lower level when the player is jumping */
    public float lowJumpMultiplier = 2.0f;

    /** Flag for deciding if a player is moving or not */
    private bool _isMoving = false;

    /** Flag for deciding if a player is running or not */
    private bool _isRunning = false;

    /** Ground Check Raycast distance */
    [SerializeField] private float groundCheckDistance = 0.3f;


    /// <summary>
    /// Checks if the player is currently moving or not. Executes moving animation if player is moving.
    /// </summary>
    public bool IsMoving {
        get { return _isMoving; }
        private set {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    // TODO WEG?
    public bool IsRunning {
        get { return _isRunning; }
        private set {
            _isRunning = value;
            animator.SetBool("IsRunning", value);
        }
    }

    /// <summary>
    /// Method is called before the first frame update.
    /// Initializes important components for later access.
    /// </summary>
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        _soundManager = FindObjectOfType<SoundManager>();
        _robotPartsSpawner = this.GetComponent<RobotPartsSpawner>();
        _collider = this.GetComponent<CapsuleCollider>();

        isAlive = true;
    }

    /// <summary>
    /// Method is called once per frame.
    /// Checks whether the game is paused or not, if the player is alive and executes the movement of the player.
    /// </summary>
    void Update() {
        //Disabling controls when game is paused
        if (PauseMenuController.IsPaused) {
            return;
        }

        //Setting the velocity to zero if player is dead and disabling controls
        if (!this.isAlive) {
            rb.velocity = Vector3.zero;
            return;
        }

        Jump();
        Move();
        CheckIsGround();

        //Increase Gravity when falling, keep it lower, when high jumping
        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (rb.velocity.y > 0.0f && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }

    /// <summary>
    /// Handles the horizontal movement of the player. Rotates the player model in the correct position depending on
    /// in which direction the player moves.
    /// </summary>
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

    /// <summary>
    /// Handles the vertical movement of the player. Executes the jumping animation.
    /// </summary>
    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            animator.SetTrigger("JumpTrigger");
            if (_soundManager) {
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


    /// <summary>
    /// Handles the event in which the player dies.
    /// Executes the death animation.
    /// </summary>
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

        _collider.enabled = false;
    }

    /// <summary>
    /// Checks if the player is currently on the ground.
    /// </summary>
    private void CheckIsGround() {
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance + 0.1f);
        Debug.DrawLine(transform.position + Vector3.up * 0.1f,
            transform.position + Vector3.down * (groundCheckDistance + 0.1f), Color.red);
        animator.SetBool("isGrounded", isGrounded);
    }


    /// <summary>
    /// Resets the level three seconds after the player dies.
    /// </summary>
    /// <returns>TODO</returns>
    private IEnumerator ResetAfterDelay() {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}