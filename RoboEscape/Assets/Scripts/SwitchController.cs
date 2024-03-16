using System.Collections.Generic;
using UnityEngine;

public class SwitchController : TriggerInterface {
    [SerializeField] private float initTimer = 1.0f;
    private float _currentTimer;
    [SerializeField] private Door door;

    public bool receiveDeadlyLaser;
    [SerializeField] private Material bodyMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Light pointLight;

    // Start is called before the first frame update
    void Start() {
        CableController = GetComponent<CableController>();
        gameObject.GetComponentInChildren<MeshRenderer>().SetMaterials(new List<Material>()
            { bodyMaterial, (receiveDeadlyLaser ? redMaterial : greenMaterial) });
        SoundManager = FindObjectOfType<SoundManager>();

        _currentTimer = initTimer;
        pointLight.color = receiveDeadlyLaser ? redMaterial.color : greenMaterial.color;
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
            _currentTimer -= Time.deltaTime;
            if (_currentTimer <= 0.0f) {
                isActivated = false;
            } else {
                UpdateDoorState(true);
                CableController.UpdateState(isActivated);
            }
        } else {
            _currentTimer = initTimer;
            UpdateDoorState(false);
            CableController.UpdateState(isActivated);
        }

        if (door && door.GetIsOpen() != initialDoorState) {
            if (SoundManager) {
                SoundManager.PlaySound("Door");
            }
        }
    }

    private void UpdatePointLightState() {
        if (isActivated) {
            if (!pointLight.enabled) {
                pointLight.enabled = true;
            }
        } else {
            if (pointLight.enabled) {
                pointLight.enabled = false;
            }
        }
    }

    private void UpdateDoorState(bool open) {
        if (door) {
            if (open) {
                door.Open();
            } else {
                door.Close();
            }
        }
    }

    public void ActivateSwitch() {
        isActivated = true;
        _currentTimer = initTimer;
    }
}