using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private bool m_IsActive;
    [SerializeField] private float initTimer = 1.0f;
    private float m_CurrentTimer;
    [SerializeField] private Door door;
    private CableController _cableController;

    public bool receiveDeadlyLaser;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Light pointLight;
    private SoundManager _soundManager;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTimer = initTimer;
        _cableController = this.GetComponent<CableController>();
        
        //TODO: Fix this somehow
        this.GetComponentInChildren<Renderer>().materials[1] = receiveDeadlyLaser ? redMaterial : greenMaterial;
        this._soundManager = FindObjectOfType<SoundManager>();

        this.pointLight.color = receiveDeadlyLaser ? redMaterial.color : greenMaterial.color;
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
                _cableController.UpdateState(m_IsActive);
            }

        }
        else
        {
            m_CurrentTimer = initTimer;
            door.Close();
            _cableController.UpdateState(m_IsActive);
        }

        if (door.GetIsOpen() != initialDoorState)
        {
            Debug.Log(door.GetIsOpen() != initialDoorState);
            _soundManager.PlaySound("Door");
        }

        UpdatePointLightState();
    }

    private void UpdatePointLightState() {
        if (m_IsActive) {
            if (!pointLight.enabled) {
                pointLight.enabled = true;
            }
        }
        else {
            if (pointLight.enabled) {
                pointLight.enabled = false;
            }
        }
    }

    public void ActivateSwitch()
    {
        m_IsActive = true;
        m_CurrentTimer = initTimer;
    }
}
