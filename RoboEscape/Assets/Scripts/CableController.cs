using UnityEngine;

/// <summary>
/// Controller for the cables.
///
/// @author Jonathan Jusup (cgt104707)
/// </summary>
public class CableController : MonoBehaviour {

    /** Container for the cables */
    [SerializeField] private GameObject cableContainer;
    
    /** Array for the segments of the cable */
    private MeshRenderer[] cableSegments;
    
    /** Activation state of the cables */
    private bool state = false;
    
    /** Inactive color */
    [SerializeField] private Color colorInactive = Color.black;
    
    /** Active color */
    [SerializeField] private Color colorActive = Color.cyan;
    
    /// <summary>
    /// Method gets called before the first frame update.
    /// Iterates over the cable segments and assigns their color. 
    /// </summary>
    void Start() {
        if (!cableContainer) {
            return;
        }
        
        int numCableSegments = cableContainer.transform.childCount;
        cableSegments = new MeshRenderer[numCableSegments];

        for (int i = 0; i < numCableSegments; i++) {
            cableSegments[i] = cableContainer.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            cableSegments[i].material.color = colorInactive;
        }
    }

    /// <summary>
    /// Updates state and changes all cable materials based on that state.
    /// Returns early, if no state change occured
    /// </summary>
    /// <param name="state">New state</param>
    public void UpdateState(bool state) {
        bool stateHasChanged = this.state != state;
        if (!cableContainer || !stateHasChanged) {
            return;
        }
        
        this.state = state;
        
        //Change cable materials based on curren state
        foreach (MeshRenderer cableSegment in cableSegments) {
            cableSegment.material.color = this.state ? colorActive : colorInactive;
        }
    }
}
