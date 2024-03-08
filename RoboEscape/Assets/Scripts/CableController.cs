using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableController : MonoBehaviour {

    [SerializeField] private GameObject cableContainer;
    private MeshRenderer[] cableSegments;
    private bool state = false;
    
    [SerializeField] private Color colorOff = Color.black;
    [SerializeField] private Color colorOn = Color.cyan;
    
    // Start is called before the first frame update
    void Start() {
        int numCableSegments = cableContainer.transform.childCount;
        cableSegments = new MeshRenderer[numCableSegments];

        for (int i = 0; i < numCableSegments; i++) {
            cableSegments[i] = cableContainer.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
            cableSegments[i].material.color = colorOff;
        }
    }

    public void UpdateState(bool state) {
        this.state = state;

        foreach (MeshRenderer cableSegment in cableSegments) {
            cableSegment.material.color = this.state ? colorOn : colorOff;
        }
    }
}
