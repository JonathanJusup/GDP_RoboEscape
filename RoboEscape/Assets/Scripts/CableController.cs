using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CableController : MonoBehaviour {

    [SerializeField] private GameObject cableContainer;
    private MeshRenderer[] cableSegments;
    private bool state = false;
    
    [SerializeField] private Color colorInactive = Color.black;
    [SerializeField] private Color colorActive = Color.cyan;
    
    // Start is called before the first frame update
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
