using UnityEngine;

/// <summary>
/// Class for the falling scrap decoration in levels. If scrap falls under a
/// minimum threshold, resets it to its initial position for the cycle to
/// repeat indefinitely
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class Scrap : MonoBehaviour {
    /** Lowest position for the falling object, before position reset */
    [SerializeField] private float lowerPosY;

    /** Starting position */
    private Vector3 _startPos;

    /** RigidBody of the object */
    private Rigidbody _rb;

    /// <summary>
    /// Method gets called before the first frame update.
    /// Sets the start position and the rigidBody of the object 
    /// </summary>
    void Start() {
        _startPos = transform.position;
        _rb = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Method gets called once per frame.
    /// Resets the objects velocity to zero and its position to its initial
    /// starting position, if it has fallen below the minimum threshold.
    /// </summary>
    void Update() {
        if (transform.position.y < lowerPosY) {
            _rb.velocity = new Vector3(0, 0, 0);
            transform.position = _startPos;
        }
    }
}