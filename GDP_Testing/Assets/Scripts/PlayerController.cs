using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 5.0f;
    public bool isOnGround = true;
    private float horizontalMovement;
    private Rigidbody rb;
    public bool dies;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input
        horizontalMovement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isOnGround = false;
        }
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalMovement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    public void die(bool isDeadly)
    {
       GameObject player = GameObject.Find("Player");
       if (isDeadly)
       {
           player.SetActive(false);    
       }
       
    }
}
