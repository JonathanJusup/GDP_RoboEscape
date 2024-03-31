using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class for a laser segment that gets shot from the laser source.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class Laser : MonoBehaviour {
    /** Flag if the laser is deadly */
    public bool isDeadly;

    /** Position and direction in which the laser will be shot */
    private Vector3 _pos, _dir;

    /** Material of the laser */
    private Material _mat;

    /** Line renderer for the laser */
    private LineRenderer _lineRenderer;

    /** Reference to the source of the laser */
    private LaserSourceController _sourceController;

    /** Green color for non-lethal laser */
    private readonly Color _greenColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);

    /** Red color for deadly laser */
    private readonly Color _redColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);


    /// <summary>
    /// Initializes the laser and its lineRenderer.
    /// </summary>
    /// <param name="position">Laser position</param>
    /// <param name="direction">Laser direction</param>
    /// <param name="parent">Laser parent</param>
    /// <param name="material">Laser material</param>
    /// <param name="isDeadly">Flag, if laser is deadly or safe</param>
    /// <param name="sourceController">Controller of laserSource</param>
    public void InitLaser(Vector3 position, Vector3 direction, Transform parent, Material material, bool isDeadly,
        LaserSourceController sourceController) {
        this._pos = position;
        this._dir = direction;
        this.transform.parent = parent;
        this._mat = material;
        this.isDeadly = isDeadly;
        this._sourceController = sourceController;
        this.gameObject.layer = LayerMask.NameToLayer("Laser");

        InitLineRenderer();
    }

    /// <summary>
    /// Initializes a lineRenderer. Depending on if the laser is deadly or not, the line renderer will have
    /// a green or red color. Sends out a rayCast and checks what has been hit to determine possible next
    /// laser segment.
    /// </summary>
    private void InitLineRenderer() {
        _lineRenderer = this.AddComponent<LineRenderer>();
        _lineRenderer.material = _mat;
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.2f;
        _lineRenderer.SetPosition(0, _pos);

        if (isDeadly) {
            //Setting red material if laser is deadly
            _lineRenderer.material.color = _redColor;
            _lineRenderer.startColor = _redColor;
            _lineRenderer.endColor = _redColor;
        } else {
            //Setting green material if laser is not deadly
            _lineRenderer.material.color = _greenColor;
            _lineRenderer.startColor = _greenColor;
            _lineRenderer.endColor = _greenColor;
        }

        // Setting up RayCast
        Ray ray = new Ray(_pos, _dir);
        RaycastHit hit;

        int layerMask = ~ LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(ray, out hit, 100, layerMask)) {
            // Processing hit if lineRenderer hit something
            ProcessHit(hit);
        } else {
            _lineRenderer.SetPosition(1, ray.GetPoint(100));
        }
    }

    /// <summary>
    /// Updates the position of the particles. Offsets position slightly to
    /// counter instant particle collisions.
    /// </summary>
    /// <param name="hit">Object hit by the rayCast</param>
    private void UpdateParticlePosition(RaycastHit hit) {
        _sourceController.UpdateParticles(hit.point - _dir * 0.05f + hit.normal * 0.05f, _lineRenderer.startColor);
    }

    /// <summary>
    /// Processes a hit from a rayCast. Leads to either transmission through a
    /// transparent object, reflection on an reflective object or absorption otherwise.
    /// Every hit case is handled here.
    /// </summary>
    /// <param name="hit">Object hit by the rayCast</param>
    private void ProcessHit(RaycastHit hit) {
        _lineRenderer.SetPosition(1, hit.point);

        //Create Child Laser depending on what is hit        
        if (hit.collider.gameObject.CompareTag("Mirror")) {
            LaserReflection(hit);
        }

        //Transmitting laser
        else if (hit.collider.gameObject.CompareTag("Transparent_Red")) {
            LaserTransmission(hit, true);
        } else if (hit.collider.gameObject.CompareTag("Transparent_Green")) {
            LaserTransmission(hit, false);
        } else if (hit.collider.gameObject.CompareTag("Transparent")) {
            LaserTransmission(hit, isDeadly);
        }

        //Colliding with player results in death of the player if laser is deadly
        else if (hit.collider.gameObject.CompareTag("Player") && this.isDeadly) {
            UpdateParticlePosition(hit);
            PlayerController playerController = hit.collider.gameObject.GetComponent<PlayerController>();
            playerController.Die();
        }

        //Destroying player props if laser is deadly
        else if (hit.collider.gameObject.CompareTag("PlayerProp") && this.isDeadly) {
            UpdateParticlePosition(hit);
            PlayerPropController propController = hit.collider.gameObject.GetComponent<PlayerPropController>();
            propController.DieAfterDelay();
        }

        //Melting objects if laser is deadly
        else if (hit.collider.gameObject.CompareTag("Meltable") && this.isDeadly) {
            UpdateParticlePosition(hit);
            MeltingController meltingController = hit.collider.gameObject.GetComponent<MeltingController>();
            meltingController.StartMelting();
        }

        //Switch has been hit, opening door
        else if (hit.collider.gameObject.CompareTag("Switch")) {
            UpdateParticlePosition(hit);
            SwitchController switchController = hit.collider.gameObject.GetComponent<SwitchController>();
            if ((switchController.receiveDeadlyLaser && isDeadly) ||
                (!switchController.receiveDeadlyLaser && !isDeadly)) {
                switchController.ActivateSwitch();
            }
        } else {
            //Laser ends here, no further reflection or transmission
            UpdateParticlePosition(hit);
        }
    }


    /// <summary>
    /// Calculates Laser reflection for mirror objects. Creates reflection
    /// child under parent object in a hierarchical structure.
    /// </summary>
    /// <param name="hit"> Actual hit position </param>
    private void LaserReflection(RaycastHit hit) {
        //Create new child under parent object
        GameObject reflection = new GameObject("Reflection");
        reflection.transform.parent = transform;

        //Set position and direction of outgoing reflection
        reflection.transform.position = hit.point;
        reflection.transform.right = Vector3.Reflect(_dir, hit.normal);

        //Add laser component with new position, direction and otherwise same attributes 
        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, this.transform,
            _mat, isDeadly, _sourceController);
    }


    /// <summary>
    /// Calculates Laser transmission for transparent objects likes glass or glass-boxes.
    /// </summary>
    /// <param name="hit">Actual hit position</param>
    /// <param name="isDeadly">isDeadly Flag</param>
    private void LaserTransmission(RaycastHit hit, bool isDeadly) {
        //Create new child under parent object
        GameObject reflection = new GameObject("Transmission");
        reflection.transform.parent = transform;

        //Set position (+ offset) and new direction of outgoing transmission
        reflection.transform.position = hit.point + _dir * 0.01f;
        reflection.transform.right = _dir;

        //Add laser component with new position, direction, isDeadlyFlag and otherwise same attributes 
        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, this.transform,
            _mat, isDeadly, _sourceController);
    }
}