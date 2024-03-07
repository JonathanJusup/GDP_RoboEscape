using Unity.VisualScripting;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private bool m_IsActive;
    [SerializeField] private float initTimer = 1.0f;
    private float m_CurrentTimer;
    [SerializeField] private Door door;
    private CableController cableController;

    public bool receiveDeadlyLaser;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTimer = initTimer;
        cableController = this.GetComponent<CableController>();
        
        //TODO: Fix this somehow
        this.GetComponentInChildren<Renderer>().materials[1] = receiveDeadlyLaser ? redMaterial : greenMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        bool initialDoorState = door.GetIsOpen();
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
                cableController.UpdateState(m_IsActive);
            }

        }
        else
        {
            m_CurrentTimer = initTimer;
            door.Close();
            cableController.UpdateState(m_IsActive);
        }

        if (door.GetIsOpen() != initialDoorState)
        {
            Debug.Log(door.GetIsOpen() != initialDoorState);
            FindObjectOfType<SoundManager>().PlaySound("Door");
        }
    }

    public void ActivateSwitch()
    {
        m_IsActive = true;
        m_CurrentTimer = initTimer;
    }
}
