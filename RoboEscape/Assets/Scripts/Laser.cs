using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class for the laser that gets shot from the laser source.
///
/// @author Jonathan Jusup (cgt104707), TODO Haben Flo und Sam hier auch mal was gemacht?
/// </summary>
public class Laser : MonoBehaviour
{
    /** Flag if the laser is deadly */
    public bool isDeadly;
    
    /** Position and direction in which the laser will be shot */
    private Vector3 pos, dir;
    
    /** Material of the laser */
    private Material mat;
    
    /** Line renderer for the laser */
    private LineRenderer lineRenderer;
    
    /** Reference to the source of the laser */
    private LaserSourceController sourceController;

    /** Green color for non-lethal laser */
    private Color greenColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    
    /** Red color for deadly laser */
    private Color redColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);


   


    /// <summary>
    /// Initializes the laser with its position, direction, layer, controller, whether it is deadly or not
    /// and initializes a line renderer.
    /// </summary>
    /// <param name="position"> Position of the laser </param>
    /// <param name="direction"> Direction of the laser </param>
    /// <param name="parent"> Parent of the laser </param>
    /// <param name="material"> Material of the laser </param>
    /// <param name="isDeadly"> Bool if the laser is deadly or not </param>
    /// <param name="sourceController"> Controller of the laser </param>
    public void InitLaser(Vector3 position, Vector3 direction, Transform parent, Material material, bool isDeadly, LaserSourceController sourceController)
    {
        
        this.pos = position;
        this.dir = direction;
        this.transform.parent = parent;
        this.mat = material;
        this.isDeadly = isDeadly;
        this.sourceController = sourceController;
        this.gameObject.layer = LayerMask.NameToLayer("Laser");
        
        InitLineRenderer();
    }

    /// <summary>
    /// Initializes a line renderer. Depending on if the laser is deadly or not, the line renderer will have
    /// a green or red color. Sends out a raycast and checks what has been hit.
    /// </summary>
    private void InitLineRenderer()
    {
        
        lineRenderer = this.AddComponent<LineRenderer>();
        lineRenderer.material = mat;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.SetPosition(0, pos);

        if (isDeadly)
        {
            // Setting red material if laser is deadly
            lineRenderer.material.color = redColor;
            lineRenderer.startColor = redColor;
            lineRenderer.endColor = redColor;
        }
        else
        {
            // Setting green material if laser is not deadly
            lineRenderer.material.color = greenColor;
            lineRenderer.startColor = greenColor;
            lineRenderer.endColor = greenColor;
        }
        
        // Setting up raycast
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        
        int layerMask =~ LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            // Processing hit if line renderer hit something
            ProcessHit(hit);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.GetPoint(100));
        }
    }

    /// <summary>
    /// Updates the position of the particles.
    /// </summary>
    /// <param name="hit"> Object hit by the raycast </param>
    private void UpdateParticlePosition(RaycastHit hit) {
        sourceController.UpdateParticles(hit.point - dir * 0.05f + hit.normal * 0.05f, lineRenderer.startColor);
    }

    /// <summary>
    /// Processes a hit from a raycast. Leads to either transmission on a transparent object, reflection on an object
    /// with reflective properties, melting if an object can melted or death on hit with the player if the laser
    /// is deadly.
    /// </summary>
    /// <param name="hit"> Object hit by the raycast </param>
    private void ProcessHit(RaycastHit hit)
    {
        lineRenderer.SetPosition(1, hit.point);

        //Create Child Laser depending on what is hit        
        if (hit.collider.gameObject.CompareTag("Mirror")) {
            LaserReflection(hit);
        }
        
        // Transmitting laser
        else if (hit.collider.gameObject.CompareTag("Transparent_Red")) {
            LaserTransmission(hit, true);
        } 
        else if (hit.collider.gameObject.CompareTag("Transparent_Green")) {
            LaserTransmission(hit, false);
        } 
        else if (hit.collider.gameObject.CompareTag("Transparent")) {
            LaserTransmission(hit, isDeadly);
        } 
        
        // Colliding with player results in death of the player if laser is deadly
        else if (hit.collider.gameObject.CompareTag("Player") && this.isDeadly) {
            UpdateParticlePosition(hit);
            PlayerController playerController = hit.collider.gameObject.GetComponent<PlayerController>();
            playerController.Die();
        } 
        
        // Destroying player props if laser is deadly
        else if (hit.collider.gameObject.CompareTag("PlayerProp") && this.isDeadly) {
            UpdateParticlePosition(hit);
            PlayerPropController propController = hit.collider.gameObject.GetComponent<PlayerPropController>();
            propController.DieAfterDelay();
        } 
        
        // Melting objects if laser is deadly
        else if (hit.collider.gameObject.CompareTag("Meltable") && this.isDeadly) {
            UpdateParticlePosition(hit);
            MeltingController meltingController = hit.collider.gameObject.GetComponent<MeltingController>();
            meltingController.StartMelting();
        } 
        
        // Switch has been hit, opening door
        else if (hit.collider.gameObject.CompareTag("Switch")) {
            UpdateParticlePosition(hit);
            SwitchController switchController = hit.collider.gameObject.GetComponent<SwitchController>();
            if ((switchController.receiveDeadlyLaser && isDeadly) || (!switchController.receiveDeadlyLaser && !isDeadly)) {
                switchController.ActivateSwitch();
            }
        }
        else {
            //Laser ends here, no further reflection or transmission
            UpdateParticlePosition(hit);
        }
    }

    
    /// <summary>
    /// Calculates Laser reflection for mirror objects. Creates reflection
    /// child under parent object in a hierarchical structure.
    /// </summary>
    /// <param name="hit"> Actual hit position </param>
    private void LaserReflection(RaycastHit hit)
    {
        //Create new child under parent object
        GameObject reflection = new GameObject("Reflection");
        reflection.transform.parent = transform;
        
        //Set position and direction of outgoing reflection
        reflection.transform.position = hit.point;
        reflection.transform.right = Vector3.Reflect(dir, hit.normal);

        //Add laser component with new position, direction and otherwise same attributes 
        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, this.transform, mat, isDeadly, sourceController);
    }
    
    
    /// <summary>
    /// Calculates Laser transmission for transparent objects likes glass or glass-boxes.
    /// </summary>
    /// <param name="hit"> Actual hit position </param>
    /// <param name="isDeadly"> death Flag for player </param>
    private void LaserTransmission(RaycastHit hit, bool isDeadly)
    {
        //Create new child under parent object
        GameObject reflection = new GameObject("Transmission");
        reflection.transform.parent = transform;
        
        //Set position (+offset) and new direction of outgoing transmission
        reflection.transform.position = hit.point + dir * 0.01f;
        reflection.transform.right = dir;

        //Add laser component with new position, direction, isDeadlyFlag and otherwise same attributes 
        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, this.transform, mat, isDeadly, sourceController);
    }
}
