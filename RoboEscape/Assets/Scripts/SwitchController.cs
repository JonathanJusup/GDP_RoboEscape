using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;

public class SwitchController : Trigger {

    
    
    [SerializeField] private float initTimer = 1.0f;
    private float m_CurrentTimer;
    [SerializeField] private Door door;
    private CableController _cableController;

    public bool receiveDeadlyLaser;
    [SerializeField] private Material bodyMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Light pointLight;
    private SoundManager _soundManager;
    
    
    
    // Start is called before the first frame update
    void Start() {
        m_CurrentTimer = initTimer;
        _cableController = this.GetComponent<CableController>();
        
        //TODO: Fix this somehow
        //gameObject.GetComponentInChildren<MeshRenderer>().materials[1] = (receiveDeadlyLaser ? redMaterial : greenMaterial);
        gameObject.GetComponentInChildren<MeshRenderer>().SetMaterials(new List<Material>() {bodyMaterial, (receiveDeadlyLaser ? redMaterial : greenMaterial)});
        this._soundManager = FindObjectOfType<SoundManager>();

        this.pointLight.color = receiveDeadlyLaser ? redMaterial.color : greenMaterial.color;
    }

    // Update is called once per frame
    void Update() {
        UpdateSwitchableState();
        UpdatePointLightState();
    }

    private void UpdateSwitchableState() {
        bool initialDoorState = false;
        if (door) {
            initialDoorState = door.GetIsOpen();
        }
        
        
        if (isActivated) {
            m_CurrentTimer -= Time.deltaTime;
            if (m_CurrentTimer <= 0.0f) {
                mIsActivated = false;
            }
            else {
                if (door) {
                    door.Open();
                }
                _cableController.UpdateState(mIsActivated);
            }
        }
        else {
            m_CurrentTimer = initTimer;
            if (door) {
                door.Close();
            }
            _cableController.UpdateState(mIsActivated);
        }

        if (door && door.GetIsOpen() != initialDoorState) {
            _soundManager.PlaySound("Door");
        }
        
        
    }

    private void UpdatePointLightState() {
        if (mIsActivated) {
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

    public void ActivateSwitch() {
        mIsActivated = true;
        m_CurrentTimer = initTimer;
    }
}