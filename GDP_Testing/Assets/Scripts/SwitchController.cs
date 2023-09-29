using Unity.VisualScripting;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private bool m_IsActive;
    [SerializeField] private float initTimer = 1.0f;
    private float m_CurrentTimer;
    [SerializeField] private Door door;

    public bool receiveDeadlyLaser;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTimer = initTimer;
        
        //TODO: Fix this somehow
        this.GetComponentInChildren<Renderer>().materials[1] = receiveDeadlyLaser ? redMaterial : greenMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsActive)
        {
            m_CurrentTimer -= Time.deltaTime;
            if (m_CurrentTimer <= 0.0f)
            {
                m_IsActive = false;
            }
            else
            {
                door.Open();
            }

        }
        else
        {
            m_CurrentTimer = initTimer;
            door.Close();
        }
    }

    public void ActivateSwitch()
    {
        m_IsActive = true;
        m_CurrentTimer = initTimer;
    }
}
