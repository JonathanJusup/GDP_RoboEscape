using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for the switch that controls the doors.
///
/// @author Jonathan El Jusup (cgt104707), Florian Kern (cgt104661)
/// </summary>
public class SwitchController : Trigger {
    /** Initial set timer */
    [SerializeField] private float initTimer = 1.0f;

    /** Current elapsed time, after timer reset */
    private float _currentTimer;

    /** Door reference that is controlled by the switch */
    [SerializeField] private Door door;

    /** Flag, if the switch receives a green (safe) or red (deadly) laser */
    public bool receiveDeadlyLaser;

    /** Actual material of the body */
    [SerializeField] private Material bodyMaterial;

    /** Green material for the switch */
    [SerializeField] private Material greenMaterial;

    /** Red material for the switch */
    [SerializeField] private Material redMaterial;

    /** Point light of the switch */
    [SerializeField] private Light pointLight;

    /// <summary>
    /// Method gets called before the first frame update.
    /// Sets the materials of the laser depending on if the switch receives a lethal laser or not.
    /// Sets the color of the point light.
    /// </summary>
    void Start() {
        CableController = GetComponent<CableController>();
        gameObject.GetComponentInChildren<MeshRenderer>().SetMaterials(new List<Material>()
            { bodyMaterial, (receiveDeadlyLaser ? redMaterial : greenMaterial) });
        SoundManager = FindObjectOfType<SoundManager>();

        _currentTimer = initTimer;
        pointLight.color = receiveDeadlyLaser ? redMaterial.color : greenMaterial.color;
    }

    /// <summary>
    /// Method gets called once per frame.
    /// Updates the state of the cables, opens the door related to the switch
    /// and updates the state of the pointlight.
    /// </summary>
    void Update() {
        UpdateSwitchableState();
        UpdatePointLightState();
    }

    /// <summary>
    /// Method opens or closes the door related to the switch.
    /// Plays a sound when the door is opened or closed.
    /// </summary>
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

    /// <summary>
    /// Enables or disables the pointLight inside of the switch.
    /// </summary>
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

    /// <summary>
    /// Opens or closes the door, if door is referenced.
    /// </summary>
    /// <param name="open"> Flag, if the door opens or closes</param>
    private void UpdateDoorState(bool open) {
        if (door) {
            if (open) {
                door.Open();
            } else {
                door.Close();
            }
        }
    }

    /// <summary>
    /// Activates the switch and resets timer to initial value
    /// </summary>
    public void ActivateSwitch() {
        isActivated = true;
        _currentTimer = initTimer;
    }
}