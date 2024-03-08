using UnityEngine;

public class SurveilanceCameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float m_RotationSpeed = 0.5f;
    
    private Vector3 m_Position;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        
        Ray ray = new Ray(m_Position, direction.normalized);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            if (hit.transform == target)
            {
                Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_RotationSpeed);            }
        }
       
    }
}
