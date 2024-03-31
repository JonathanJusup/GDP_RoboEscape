using UnityEngine;

/// <summary>
/// Controller for mirrors that reflect lasers. A mirror can be rotated
/// clockwise and/or counter clockwise. To compensate lower pivot, mirror
/// can be translated optionally.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class MirrorController : MonoBehaviour {
    /** Trigger that rotates the mirror to the counter clockwise */
    [SerializeField] private PressurePlate leftTrigger;

    /** Trigger that rotates the mirror to the clockwise */
    [SerializeField] private PressurePlate rightTrigger;

    /** Transform of the mirror base, which doesn't rotate */
    [SerializeField] private Transform translationCenter;

    /** Transform of the mirror body, which rotates*/
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

    /** Lower shift offset in WorldSpace */
    private float _offsetLower;

    /** Upper shift offset in WorldSpace */
    private float _offsetUpper;

    /** Mirror animator */
    private Animator _animator;

    /** Particle system */
    private ParticleSystem _particleSystem;


    /// <summary>
    /// Method gets called before the first frame update.
    /// Sets the animator, particleSystem and calculates shift bounds in world space.
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
    /// Rotates the mirror and offsets its translation center based
    /// on its current rotation between its bounds.
    /// </summary>
    void Update() {
        //Early return, if no trigger assigned
        if (!leftTrigger && !rightTrigger) {
            return;
        }

        CalcMirrorRotation();

        //Keep rotation between 0..360
        float currentZRotation = body.rotation.eulerAngles.z;
        if (currentZRotation > 180f) {
            currentZRotation -= 360f;
        }

        //Calculate offset by interpolating shiftBounds based on current rotation between bounds.
        float offsetX = Mathf.Lerp(shiftLower, shiftUpper,
            (currentZRotation - rotationLower) / (rotationUpper - rotationLower));

        //Cancel offset, if bounds are equal
        if (shiftLower.Equals(shiftUpper)) {
            offsetX = shiftLower;
        }

        //Apply shift horizontally or vertically
        if (shiftHorizontally) {
            translationCenter.position =
                new Vector3(offsetX, translationCenter.position.y, translationCenter.position.z);
        } else {
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

        //If both triggers are assigned
        if (leftTrigger && rightTrigger) {
            if (leftTrigger.getIsActivated && !rightTrigger.getIsActivated) {
                //Rotate CounterClockwise
                rotationFactor = 1.0f;
            } else if (!leftTrigger.getIsActivated && rightTrigger.getIsActivated) {
                //Rotate Clockwise
                rotationFactor = -1.0f;
            }

            if (leftTrigger.getIsActivated && rightTrigger.getIsActivated) {
                _animator.SetBool("IsBlocked", true);
                _particleSystem.Play();
            } else {
                _animator.SetBool("IsBlocked", false);
                _particleSystem.Stop();
            }
        } else if (leftTrigger && leftTrigger.getIsActivated) {
            //If only left trigger is assigned and pressed
            rotationFactor = 1.0f;
        } else if (rightTrigger && rightTrigger.getIsActivated) {
            //If only right trigger is assigned and pressed
            rotationFactor = -1.0f;
        }

        _currentRotation += rotationFactor * rotationSpeed * Time.deltaTime;
        _currentRotation = Mathf.Clamp(_currentRotation, _offsetLower, _offsetUpper);
        body.rotation = _initRotation * Quaternion.Euler(0.0f, 0.0f, _currentRotation);
    }
}