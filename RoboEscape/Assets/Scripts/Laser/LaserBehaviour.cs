using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LaserBehaviour : MonoBehaviour {
    private Vector3 pos;
    private Vector3 dir;
    private bool isDeadly;
    private LaserSourceController source;

    private LineRenderer lineRenderer;
    [SerializeField] private Color colorDeadly = Color.red;
    [SerializeField] private Color colorSafe = Color.green;


    // Start is called before the first frame update
    void Awake() {
        this.lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update() { }

    public void InitLaser(Vector3 position, Vector3 direction, bool isDeadly,
        LaserSourceController laserSourceController) {
        
        Debug.Log("[LASER INIT]");

        this.pos = position;
        this.dir = direction;
        this.isDeadly = isDeadly;
        this.source = laserSourceController;
        this.gameObject.layer = LayerMask.NameToLayer("TransparentFX");

        InitLineRenderer();
    }

    private void InitLineRenderer() {
        Debug.Log("[LINE RENDERER INIT]");
        
        if (isDeadly) {
            //lineRenderer.material.color = colorDeadly;
            lineRenderer.startColor = colorDeadly;
            lineRenderer.endColor = colorDeadly;
        }
        else {
            //lineRenderer.material.color = colorSafe;
            lineRenderer.startColor = colorSafe;
            lineRenderer.endColor = colorSafe;
        }
        
        Debug.Log("[LINE RENDERER AFTER COLOR SET]");

        lineRenderer.SetPosition(0, pos);
        this.gameObject.layer = 1;


        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100)) {
            ProcessHit(hit);
        }
        else {
            lineRenderer.SetPosition(1, ray.GetPoint(100));
        }
    }

    private void UpdateParticlePosition(RaycastHit hit) {
        source.UpdateParticles(hit.point - dir * 0.05f + hit.normal * 0.05f, lineRenderer.startColor);
    }

    private void ProcessHit(RaycastHit hit) {
        lineRenderer.SetPosition(1, hit.point);

        //Create Child Laser depending on what is hit        
        if (hit.collider.gameObject.CompareTag("Mirror")) {
            LaserReflection(hit);
        }
        else if (hit.collider.gameObject.CompareTag("Transparent_Red")) {
            LaserTransmission(hit, true);
        }
        else if (hit.collider.gameObject.CompareTag("Transparent_Green")) {
            LaserTransmission(hit, false);
        }
        else if (hit.collider.gameObject.CompareTag("Transparent")) {
            LaserTransmission(hit, isDeadly);
        }
        else if (hit.collider.gameObject.CompareTag("Player") && this.isDeadly) {
            UpdateParticlePosition(hit);

            //PlayerController playerController = hit.collider.gameObject.GetComponent<PlayerController>();
            PlayerController playerController =
                hit.collider.gameObject.transform.parent.parent.GetComponent<PlayerController>();
            playerController.Die();
        }
        else if (hit.collider.gameObject.CompareTag("PlayerProp") && this.isDeadly) {
            UpdateParticlePosition(hit);
            PlayerPropController propController = hit.collider.gameObject.GetComponent<PlayerPropController>();
            propController.DieAfterDelay();
        }
        else if (hit.collider.gameObject.CompareTag("Meltable") && this.isDeadly) {
            UpdateParticlePosition(hit);
            MeltingController meltingController = hit.collider.gameObject.GetComponent<MeltingController>();
            meltingController.StartMelting();
        }
        else if (hit.collider.gameObject.CompareTag("Switch")) {
            UpdateParticlePosition(hit);
            SwitchController switchController = hit.collider.gameObject.GetComponent<SwitchController>();
            if ((switchController.receiveDeadlyLaser && isDeadly) ||
                (!switchController.receiveDeadlyLaser && !isDeadly)) {
                switchController.ActivateSwitch();
            }
        }
        else {
            //Laser ends here, no further reflection or transmission
            UpdateParticlePosition(hit);
        }
    }

    /**
     * Calculates Laser reflection for mirror objects. Creates reflection
     * child under parent object in a hierarchical structure.
     *
     * @param hit Actual hit position
     */
    private void LaserReflection(RaycastHit hit) {
        /*
        //Create new child under parent object
        GameObject reflection = new GameObject("Reflection");
        reflection.transform.parent = transform;

        //Set position and direction of outgoing reflection
        reflection.transform.position = hit.point;
        reflection.transform.right = Vector3.Reflect(dir, hit.normal);

        //Add laser component with new position, direction and otherwise same attributes
        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, mat, isDeadly, sourceController);
        */

        if (this.transform.childCount > 0) {
            return;
        }
        
        GameObject reflection = source.GetLaser();
        if (!reflection) {
            Debug.Log("[ERROR] No more available Lasers in Pool!");
            return;
        }
        reflection.SetActive(true);
        
        reflection.transform.parent = this.transform.parent;
        reflection.GetComponent<LaserBehaviour>()
            .InitLaser(hit.point, Vector3.Reflect(dir, hit.normal), this.isDeadly, source);
    }

    /**
     * Calculates Laser transmission for transparent objects likes glass or glass-boxes.
     *
     * @param hit Actual hit position
     * @param isDeadly death Flag for player
     */
    private void LaserTransmission(RaycastHit hit, bool isDeadly) {
        /*
        //Create new child under parent object
        GameObject reflection = new GameObject("Transmission");
        reflection.transform.parent = transform;

        //Set position (+offset) and new direction of outgoing transmission
        reflection.transform.position = hit.point + dir * 0.01f;
        reflection.transform.right = dir;

        //Add laser component with new position, direction, isDeadlyFlag and otherwise same attributes 
        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, mat, isDeadly,
            sourceController);
        */

        if (this.transform.childCount > 0) {
            return;
        }

        GameObject transmission = source.GetLaser();
        if (!transmission) {
            Debug.Log("[ERROR] No more available Lasers in Pool!");
            return;
        }
        transmission.SetActive(true);
        
        transmission.transform.parent = this.transform.parent;
        transmission.GetComponent<LaserBehaviour>().InitLaser(hit.point + dir * 0.01f, this.dir, isDeadly, source);
    }
}