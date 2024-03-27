using UnityEngine;

/// <summary>
/// Base Trigger class with all necessary components for trigger elements,
/// such as PressurePlate and LaserSwitch, which both contain.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class Trigger : MonoBehaviour {
    /** Flag, if trigger is activated */
    protected bool isActivated;

    /// <summary>
    /// Getter for isActivated flag
    /// </summary>
    public bool getIsActivated => isActivated;

    /** Cable Controller, containing connection cables between trigger and effector */
    protected CableController CableController;

    /** Sound Manager */
    protected SoundManager SoundManager;
}