using UnityEngine;


/// <summary>
/// Surveillance camera controller. If possible, surveillance camera
/// should always look at the player. If line of sight is interrupted,
/// camera stops rotating towards the player.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class SurveillanceCameraController : MonoBehaviour {
    /** Transform of the cameraBody, which rotates */
    [SerializeField] private Transform cameraBody;

    /** Camera rotation speed */
    [SerializeField] private float rotationSpeed = 0.5f;

    /** Target at which the camera is looking at */
    private Transform _target;

    /** Position of the camera */
    private Vector3 _position;


    /// <summary>
    /// Method gets called before the first frame update
    /// Sets the target and position of the camera.
    /// </summary>
    void Start() {
        _target = GameObject.Find("Player").transform;
        _position = cameraBody.position;
    }

    /// <summary>
    /// Method gets called once per frame.
    /// Casts a ray in the direction of the player at a certain range, if the player
    /// is inside of that range, the camera will follow the player.
    /// </summary>
    void Update() {
        // Setting direction of ray
        Vector3 direction = _target.position - cameraBody.position;

        // Initializing ray with camera position and direction
        Ray ray = new Ray(_position, direction.normalized);
        RaycastHit hit;

        // Casting ray
        if (Physics.Raycast(ray, out hit, 30, 1)) {
            if (hit.transform == _target) {
                // Rotating camera to match player movements
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
                cameraBody.rotation =
                    Quaternion.Slerp(cameraBody.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}