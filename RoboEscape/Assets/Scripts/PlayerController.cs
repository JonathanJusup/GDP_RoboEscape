using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float speed = 5.0f;
    public float jumpPower = 5.0f;
    public bool isGrounded = true;
    public bool isAlive = true;
    private float horizontalMovement;
    private Rigidbody rb;
    Animator animator;
    private SoundManager _soundManager;
    private RobotPartsSpawner _robotPartsSpawner;
    
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    private bool _isMoving = false;
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

    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        _soundManager = FindObjectOfType<SoundManager>();
        _robotPartsSpawner = this.GetComponent<RobotPartsSpawner>();

        isAlive = true;
    }

    // Update is called once per frame
    void Update() {
        if (PauseMenuController.IsPaused) {
            return;
        }

        if (!this.isAlive) {
            rb.velocity = Vector3.zero;
            return;
        }
        
        animator.SetBool("isGrounded", isGrounded);
        
        Jump();
        Move();

        //Increase Gravity when falling, keep it lower, when high jumping
        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (rb.velocity.y > 0.0f && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }


    /*
    private void FixedUpdate() {
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalMovement);

        // Check if the player is moving to the left and flip the character
        if (horizontalMovement < 0) {
            transform.Translate(Vector3.back * Time.deltaTime * speed * horizontalMovement);
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
        // Check if the player is moving to the right and flip the character back
        else if (horizontalMovement > 0) {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalMovement);
            transform.rotation = transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
    */
    
    void Move() {
        float horizontalMovement = Input.GetAxis("Horizontal");
        IsMoving = Mathf.Abs(horizontalMovement) > 0.1f;

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, 0.0f);
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0.0f);
        
        //Rotate player accoring to movement direction
        if (horizontalMovement < 0) {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        } else if (horizontalMovement > 0) {
            transform.rotation = transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
    
    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            //Reset y-velocity before jumping
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(velocity.x, 0.0f, velocity.z);
            rb.velocity = velocity;
            
            //Execute jump
            rb.velocity = Vector3.up * jumpPower; 
            isGrounded = false;

            animator.SetTrigger("JumpTrigger");
            _soundManager.PlaySound("Jump");
        }
    }
    
    public void Die() {
        if (!isAlive) {
            return;
        }
        
        Debug.Log("PLAYER DEATH");
        isAlive = false;
        
        animator.SetTrigger("DeathTrigger");
        
        //player.SetActive(false);
        Vector3 playerPos = transform.position;
        _robotPartsSpawner.SpawnParts(playerPos);
        StartCoroutine(ResetAfterDelay());
        _soundManager.PlaySound("PlayerDeath");
    }

    public void SetIsGrounded(bool isGround) {
        isGrounded = isGround;
    }
    
    private IEnumerator ResetAfterDelay()
    {
        Debug.Log("Reset in 3 Seconds");
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}