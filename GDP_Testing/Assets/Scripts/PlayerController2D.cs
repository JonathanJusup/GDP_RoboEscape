using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 5.0f;
    public bool isGrounded = true;
    private Rigidbody rb;
    public bool dies;
    
   
    
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

        if (!PauseMenuController.IsPaused)
        {
            Jump();
            Move();
        
            // Hier wird die Gravitation erhöht, um den Fall zu beschleunigen
            if (rb.velocity.y < 0) {
                rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
            } else if (rb.velocity.y > 0.0f && !Input.GetButton("Jump")) {
                rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
            }
        }
           

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) { 
            Die();
        }
    }

    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            /*
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(velocity.x, 0.0f, velocity.z);
            rb.velocity = velocity;
            
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
            */
            
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(velocity.x, 0.0f, velocity.z);
            rb.velocity = velocity;
            
            rb.velocity = Vector3.up * jumpPower; 
            isGrounded = false;

        }
    }
    
    void Move() {
        float horizontalMovement = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, 0.0f);
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0.0f);
    }
    

    public void Die()
    {
        // TODO add death sound, preferably lego
        //TODO: No need to "find" player, its already the gameObject itself
        GameObject player = GameObject.Find("Player");
       
       player.SetActive(false);
       Vector3 playerPos = transform.position;
       CubeSpawner cubeSpawner = GameObject.Find("CubeSpawn").GetComponent<CubeSpawner>();
       cubeSpawner.SpawnCubes(playerPos);

       
    }

    public void SetIsGrounded(bool isGround) {
        isGrounded = isGround;
    }
}
