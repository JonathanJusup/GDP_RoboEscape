using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5.0f;
    public float jumpPower = 5.0f;
    public bool isGrounded = true;
    private float horizontalMovement;
    private Rigidbody rb;
    Animator animator;
    
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
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        /*
        // Get player input
        horizontalMovement = Input.GetAxis("Horizontal");

        // Check if the player is moving
        IsMoving = Mathf.Abs(horizontalMovement) > 0.1f;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        animator.SetBool("isGrounded", isGrounded);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f)) {
            Debug.Log("Raycast hit the ground at: " + hit.point + isGrounded);
        }
        else {
            Debug.Log("Raycast did not hit anything." + isGrounded + "isses");
        }

        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);


        if (Input.GetButtonDown("Jump") && isGrounded) {
            animator.SetTrigger("JumpTrigger");
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        */
        
        
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
        }
    }
    

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Bullet")) {
            Die();
        }
    }


    public void Die() {
        // TODO add death sound, preferably lego
        //TODO: No need to "find" player, its already the gameObject itself
        GameObject player = GameObject.Find("Player");
        animator.SetTrigger("DeathTrigger");
        
        //player.SetActive(false);
        Vector3 playerPos = transform.position;
        CubeSpawner cubeSpawner = GameObject.Find("CubeSpawn").GetComponent<CubeSpawner>();
        cubeSpawner.SpawnCubes(playerPos);
        
        //Zerstöre PlayerController
        Destroy(this);
    }

    public void SetIsGrounded(bool isGround) {
        isGrounded = isGround;
    }
}