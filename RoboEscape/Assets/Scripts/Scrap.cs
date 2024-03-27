using UnityEngine;

/// <summary>
/// Class for the falling scrap inside of the first level.
///
/// @author Jonathan Jusup
/// </summary>
public class Scrap : MonoBehaviour
{
    /** Lowest position for the falling object */
    [SerializeField] private float lowerPosY;
    
    /** Starting position */
    private Vector3 startPos;
    
    /** Rigidbody of the object */
    private Rigidbody rb;
    
    /// <summary>
    /// Method gets called before the first frame update.
    /// Sets the start position and the rigidbody of the object 
    /// </summary>
    void Start()
    {
        startPos = transform.position;
        rb = this.GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Method gets called once per frame.
    /// Resets the objects position if it has fallen below the value lowerPosY.
    /// </summary>
    void Update()
    {
        if (transform.position.y < lowerPosY)
        {
            rb.velocity = new Vector3(0, 0, 0);
            // Resetting position of the object to the starting position
            transform.position = startPos;
        }
    }
}
