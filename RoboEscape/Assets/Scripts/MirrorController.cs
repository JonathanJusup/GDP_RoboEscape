using UnityEngine;

/// <summary>
/// Controller for the mirrors that reflect the lasers.
///
/// @author Jonathan Jusup (cgt104707)
/// </summary>
public class MirrorController : MonoBehaviour {
    
    /** Trigger that rotates the mirror to the left */
    [SerializeField] private PressurePlate leftTrigger;
    
    /** Trigger that rotates the mirror to the right */
    [SerializeField] private PressurePlate rightTrigger;

    /** Transform of the mirror base TODO stimmt das? */
    [SerializeField] private Transform translationCenter;
    
    /** Transform of the mirror TODO stimmt das? */
    [SerializeField] private Transform body;

    /** Rotation Speed */
    [SerializeField] private float rotationSpeed;   
    
    /** Lower Rotation bound */
    [SerializeField] private float rotationLower;  
    
    /** Upper rotation bound */
    [SerializeField] private float rotationUpper;           
    
    /** Flag if Mirror moves horizontally or vertically */
    [SerializeField] private bool shiftHorizontally = true;
    
    /** Lower movement bound */
    [SerializeField] private float shiftLower = -0.5f;    
    
    /** Upper movement bound */
    [SerializeField] private float shiftUpper = 0.5f;     
    
    /** Initial rotation of the mirror */
    private Quaternion _initRotation;
    
    /** Current rotation */
    private float _currentRotation = 0.0f;
    
    /** TODO */
    private float _offsetLower;
    
    /** TODO */
    private float _offsetUpper;
    
    /** Animator of the mirror */
    private Animator _animator;
    
    /** Particle system */
    private ParticleSystem _particleSystem;



    /// <summary>
    /// Method gets called before the first frame update.
    /// Sets the animator, the particle system and shifts the mirror into the right position.
    /// </summary>
    private void Start() {
        _animator = this.GetComponentInChildren<Animator>();
        _particleSystem = this.GetComponentInChildren<ParticleSystem>();
        
        _initRotation = body.rotation;
        _offsetLower = rotationLower - body.transform.eulerAngles.z;
        _offsetUpper = rotationUpper - body.transform.eulerAngles.z;

        Vector3 initPosition = transform.position;
        shiftLower += shiftHorizontally ? initPosition.x : initPosition.y;
        shiftUpper += shiftHorizontally ? initPosition.x : initPosition.y;
    }

    /// <summary>
    /// Method gets called once per frame.
    /// Rotates the mirror and shifts it in the right direction.
    /// </summary>
    void Update() {
        if (!leftTrigger && !rightTrigger) {
            return;
        }

        CalcMirrorRotation();
        
        float currentZRotation = body.rotation.eulerAngles.z;
        if (currentZRotation > 180f) {
            currentZRotation -= 360f;
        }

        float offsetX = Mathf.Lerp(shiftLower, shiftUpper,
            (currentZRotation - rotationLower) / (rotationUpper - rotationLower));

        if (shiftLower.Equals(shiftUpper)) {
            offsetX = shiftLower;
        }

        //TODO: THIS IS UGLY
        if (shiftHorizontally) {
            translationCenter.position =
                new Vector3(offsetX, translationCenter.position.y, translationCenter.position.z);
        }
        else {
            translationCenter.position =
                new Vector3(translationCenter.position.x, offsetX, translationCenter.position.z);
        }
    }

    /// <summary>
    /// Calculates the rotation of the mirror depending on the pressed trigger.
    /// Activates the particle system if both assigned triggers are pressed simultaneously 
    /// </summary>
    private void CalcMirrorRotation() {
        float rotationFactor = 0.0f;

        if (leftTrigger && rightTrigger) {
            //If both triggers are assigned

            if (leftTrigger.getIsActivated && !rightTrigger.getIsActivated) {
                //Rotate CounterClockwise
                rotationFactor = 1.0f;
            }
            else if (!leftTrigger.getIsActivated && rightTrigger.getIsActivated) {
                //Rotate Clockwise
                rotationFactor = -1.0f;
            }
            
            if (leftTrigger.getIsActivated && rightTrigger.getIsActivated) {
                _animator.SetBool("IsBlocked", true);
                _particleSystem.Play();
            }
            else {
                _animator.SetBool("IsBlocked", false);
                _particleSystem.Stop();
            }
        }
        else if (leftTrigger && leftTrigger.getIsActivated) {
            //If only left trigger is assigned and pressed
            rotationFactor = 1.0f;
        }
        else if (rightTrigger && rightTrigger.getIsActivated) {
            //If only right trigger is assigned and pressed
            rotationFactor = -1.0f;
        }


        _currentRotation += rotationFactor * rotationSpeed * Time.deltaTime;
        _currentRotation = Mathf.Clamp(_currentRotation, _offsetLower, _offsetUpper);
        body.rotation = _initRotation * Quaternion.Euler(0.0f, 0.0f, _currentRotation);
    }
}