using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] private float lowerPosY;
    
    private Vector3 startPos;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowerPosY)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = startPos;
        }
    }
}
