using UnityEngine;

/// <summary>
/// Interface that provides access to the CableController, SoundManager and the state whether the implementing object
/// is currently activated or not.
///
/// @author Jonathan Jusup (cgt104707)
/// </summary>
public class TriggerInterface : MonoBehaviour {
    
    /** Bool that decides if the object is activated or not */
    [SerializeField] protected bool isActivated;
    
    /// <summary>
    /// Getter for the state of isActivated.
    /// </summary>
    public bool getIsActivated => isActivated;

    /** The cable controller */
    protected CableController CableController;
    
    /** The sound manager */
    protected SoundManager SoundManager;
}
