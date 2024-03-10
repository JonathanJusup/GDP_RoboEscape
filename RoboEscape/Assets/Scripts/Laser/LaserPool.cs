using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour {

    [SerializeField] private GameObject _laser;     //Laser Prefab
    [SerializeField] private int _size = 10;        //Pool size
    private List<GameObject> lasers;                //Laser Array
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.lasers = new List<GameObject>();
        
        //Initialize all Lasers, pool them and deactivate
        for (int i = 0; i < _size; i++) {
            GameObject laser = Instantiate(_laser, Vector3.zero, Quaternion.identity, this.transform);
            laser.transform.name = "Laser[" + i + "]";
            
            laser.SetActive(false);
            laser.transform.parent = this.transform;

            InitLaserParameters(laser);
            this.lasers.Add(laser);
        }
    }

    /**
     * Returns currently available laser, which is inactive and currently not in use.
     * If all lasers are active and in use, returns NULL. Caller has to set new parameters
     * and activate laser.
     * 
     * @return Available laser
     */
    public GameObject GetLaser() {
        GameObject result = null;

        //Iterate through pool and get currently available laser
        for (int i = 0; i < lasers.Count && !result; i++) {
            GameObject laser = lasers[i];
            
            if (!laser.activeSelf) {
                result = laser;
            }
        }
        
        return result;
    }

    /**
     * Resets given Laser by reseting its parameters, setting its parent to the
     * pool and deactivates it.
     * 
     * @return laser Laser to reset
     */
    public void ResetLaser(GameObject laser) {
        InitLaserParameters(laser);
        laser.SetActive(false);
        laser.transform.parent = this.transform;
    }
    
    /**
     * Initializes Laser parameters such as material color, color and endpoints.
     *
     * @param laser Laser to initialize its parameters
     */
    private void InitLaserParameters(GameObject laser) {
        //Initialize laser parameters
        LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();
            
        lineRenderer.material.color = Color.white;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
            
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(1, 0, 0));
    }
}
