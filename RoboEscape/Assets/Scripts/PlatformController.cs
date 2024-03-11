using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float moveDistance = 2.0f; // Die Entfernung, die die Plattform bewegt
    public float moveSpeed = 1.0f;    // Die Geschwindigkeit der Plattform

    private Vector2 initialPosition;
    private bool movingUp = true;

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

        if (movingUp)
        {
            transform.Translate(Vector2.up * movement);
            if (transform.position.y >= initialPosition.y + moveDistance)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector2.down * movement);
            if (transform.position.y <= initialPosition.y)
            {
                movingUp = true;
            }
        }
    }
}
