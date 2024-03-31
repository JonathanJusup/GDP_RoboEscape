using UnityEngine;

/// <summary>
/// Class for a simple conveyor belt. It adds a force to all
/// objects currently staying inside its collider, moving them
/// in a specific direction.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class ConveyorBelt : MonoBehaviour {
    /** Speed of the conveyor belt */
    public float conveyorSpeed = 1.0f;

    /// <summary>
    /// Gets called when an object stays on top of the conveyor belt.
    /// Runs the colliding object across the conveyor belt.
    /// </summary>
    /// <param name="other">Colliding object</param>
    private void OnCollisionStay(Collision other) {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null) {
            // Adding force to the colliding object
            rb.AddForce(Vector3.right * conveyorSpeed, ForceMode.Acceleration);
        }
    }
}