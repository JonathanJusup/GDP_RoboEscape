using UnityEngine;

/// <summary>
/// Controller for the cables, which connects triggers and effectors
/// Cables changes their colors depending on trigger state.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class CableController : MonoBehaviour {
    /** Container for the cables */
    [SerializeField] private GameObject cableContainer;

    /** Array for the segments of the cable */
    private MeshRenderer[] _cableSegments;

    /** Current State */
    private bool _state = false;

    /** Inactive color */
    [SerializeField] private Color colorInactive = Color.black;

    /** Active color */
    [SerializeField] private Color colorActive = Color.cyan;

    /// <summary>
    /// Method gets called before the first frame update.
    /// Iterates over all cable segments and assigns colorInactive. 
    /// </summary>
    void Start() {
        if (!cableContainer) {
            return;
        }

        int numCableSegments = cableContainer.transform.childCount;
        _cableSegments = new MeshRenderer[numCableSegments];

        for (int i = 0; i < numCableSegments; i++) {
            _cableSegments[i] = cableContainer.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            _cableSegments[i].material.color = colorInactive;
        }
    }

    /// <summary>
    /// Updates state and changes all cable materials based on that state.
    /// Returns early, if no state change occured.
    /// </summary>
    /// <param name="state">New state</param>
    public void UpdateState(bool state) {
        bool stateHasChanged = this._state != state;
        if (!cableContainer || !stateHasChanged) {
            return;
        }

        this._state = state;

        //Change cable materials based on current state
        foreach (MeshRenderer cableSegment in _cableSegments) {
            cableSegment.material.color = this._state ? colorActive : colorInactive;
        }
    }
}