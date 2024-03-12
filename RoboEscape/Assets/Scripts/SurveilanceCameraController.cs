using UnityEngine;
using UnityEngine.Serialization;

public class SurveilanceCameraController : MonoBehaviour
{

    [SerializeField] private Transform cameraBody;
    [SerializeField] private float rotationSpeed = 0.5f;
    
    private Transform _target;
    private Vector3 _position;
    
    
    // Start is called before the first frame update
    void Start() {
        _target = GameObject.Find("Player").transform;
        _position = cameraBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _target.position - cameraBody.position;
        
        Ray ray = new Ray(_position, direction.normalized);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            if (hit.transform == _target)
            {
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
                cameraBody.rotation = Quaternion.Slerp(cameraBody.rotation, lookRotation, Time.deltaTime * rotationSpeed);            
            }
        }
    }
}
