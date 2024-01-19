using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 5.0f;
    public bool isGrounded = true;
    public bool isOnGround = true;
    private float horizontalMovement;
    private Rigidbody rb;
    Animator animator;
    public bool dies;

    private bool _isMoving = false;
    private bool _isRunning = false;

    public bool IsMoving {  get
        { 
            return _isMoving; 
        } 
        private set 
        { 
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool("IsRunning", value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input
        horizontalMovement = Input.GetAxis("Horizontal");

        // Check if the player is moving
        IsMoving = Mathf.Abs(horizontalMovement) > 0.1f;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        animator.SetBool("isGrounded", isGrounded);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            Debug.Log("Raycast hit the ground at: " + hit.point + isGrounded);
            
        }
        else
        {
            Debug.Log("Raycast did not hit anything." + isGrounded + "isses");
            
        }
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
        




        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("JumpTrigger");
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
           
        }
        
    }

    
    private void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalMovement);

        // Check if the player is moving to the left and flip the character
        if (horizontalMovement < 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed * horizontalMovement);
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
        // Check if the player is moving to the right and flip the character back
        else if (horizontalMovement > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalMovement);
            transform.rotation = transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    

}
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }
    

    public void Die()
    {
        // TODO add death sound, preferably lego
        //TODO: No need to "find" player, its already the gameObject itself
        GameObject player = GameObject.Find("Player");
        animator.SetTrigger("DeathTrigger");
        //player.SetActive(false);
       Vector3 playerPos = transform.position;
       CubeSpawner cubeSpawner = GameObject.Find("CubeSpawn").GetComponent<CubeSpawner>();
       cubeSpawner.SpawnCubes(playerPos);

       
    }

    public void SetIsGrounded(bool isGround)
    {
        isOnGround = isGround;
    }
}
