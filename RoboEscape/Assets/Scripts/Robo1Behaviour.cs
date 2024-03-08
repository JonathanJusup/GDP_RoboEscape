using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Robo1Behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position; // temporäres modell hat falschen ursprung, hier fix für shooting detection
        currentPosition.y += 1f; 
         


    }

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootingRange = 10f;
    public float shootingCooldown = 3f; // time between shots
    public float bulletSpeed = 5f;
    private float lastShotTime = 0f;
    private Vector3 currentPosition;
    

    private void Update()
    {
        //check player visibility
        RaycastHit hit;
        
        //function um shootingrange zu sehen
        Debug.DrawRay(currentPosition, transform.forward * shootingRange, Color.red);
       
        if (Physics.Raycast(currentPosition, transform.forward, out hit, shootingRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // check if enough time has passed since last shot
                if (Time.time - lastShotTime >= shootingCooldown)
                {
                    Shoot();
                    Debug.Log("shooting");
                }
            }
        }
    }

    void Shoot()
    {
        lastShotTime = Time.time;

        // bullet created at firepoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = -firePoint.right * bulletSpeed;

        Destroy(bullet, 2f); // destroy bullet after time has passed
    }
    
    void OnTriggerEnter()
    { 
        Destroy(gameObject);
                
    }
}


