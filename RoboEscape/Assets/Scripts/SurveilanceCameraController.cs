using UnityEngine;


/// <summary>
/// Controller for the cameras in the level.
///
/// @author Jonathan Jusup (cgt104707)
/// </summary>
public class SurveilanceCameraController : MonoBehaviour
{

    /** Transform of the camera */
    [SerializeField] private Transform cameraBody;
    
    /** Speed at which the camera follows the player */
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
    void Update()
    {
        // Setting direction of ray
        Vector3 direction = _target.position - cameraBody.position;
        
        // Initializing ray with camera position and direction
        Ray ray = new Ray(_position, direction.normalized);
        RaycastHit hit;
        
        // Casting tay
        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            if (hit.transform == _target)
            {
                // Rotating camera to match player movements
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
                cameraBody.rotation = Quaternion.Slerp(cameraBody.rotation, lookRotation, Time.deltaTime * rotationSpeed);            
            }
        }
    }
}
