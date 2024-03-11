using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerHorizontal : MonoBehaviour
{
    public float moveDistance = 2.0f; // Die Entfernung, die die Plattform bewegt
    public float moveSpeed = 1.0f;    // Die Geschwindigkeit der Plattform

    private Vector2 initialPosition;
    private bool movingRight = true;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        float movement = moveSpeed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector2.right * movement);
            if (transform.position.x >= initialPosition.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * movement);
            if (transform.position.x <= initialPosition.x)
            {
                movingRight = true;
            }
        }
    }
}