using UnityEngine;
using UnityEngine.Serialization;

public class MirrorController : MonoBehaviour {
    [SerializeField] private Trigger leftTrigger;
    [SerializeField] private Trigger rightTrigger;

    [SerializeField] private Transform translationCenter;
    [SerializeField] private Transform body;

    [SerializeField] private float rotationSpeed;           //Rotation Speed
    [SerializeField] private float rotationLower;           //Lower Rotation bound
    [SerializeField] private float rotationUpper;           //Upper rotation bound
    
    [SerializeField] private bool shiftHorizontally = true;     //Flag, if Mirror moves horizontally or vertically
    /*[SerializeField] */private float shiftLower = -0.5f;    //Lower movement bound
    /*[SerializeField] */private float shiftUpper = 0.5f;     //Upper movement bound

    
    private Quaternion _initRotation;
    private float _currentRotation = 0.0f;
    private float _offsetLower;
    private float _offsetUpper;


    private void Start() {
        _initRotation = body.rotation;
        _offsetLower = rotationLower - body.transform.eulerAngles.z;
        _offsetUpper = rotationUpper - body.transform.eulerAngles.z;

        Vector3 initPosition = transform.position;
        shiftLower += shiftHorizontally ? initPosition.x : initPosition.y;
        shiftUpper += shiftHorizontally ? initPosition.x : initPosition.y;
    }

    // Update is called once per frame
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

    private void CalcMirrorRotation() {
        float rotationFactor = 0.0f;

        if (leftTrigger && rightTrigger) {
            //If both triggers are assigned

            if (leftTrigger.isPressed && !rightTrigger.isPressed) {
                //Rotate CounterClockwise
                rotationFactor = 1.0f;
            }
            else if (!leftTrigger.isPressed && rightTrigger.isPressed) {
                //Rotate Clockwise
                rotationFactor = -1.0f;
            }
        }
        else if (leftTrigger && leftTrigger.isPressed) {
            //If only left trigger is assigned and pressed
            rotationFactor = 1.0f;
        }
        else if (rightTrigger && rightTrigger.isPressed) {
            //If only right trigger is assigned and pressed
            rotationFactor = -1.0f;
        }


        _currentRotation += rotationFactor * rotationSpeed * Time.deltaTime;
        _currentRotation = Mathf.Clamp(_currentRotation, _offsetLower, _offsetUpper);
        body.rotation = _initRotation * Quaternion.Euler(0.0f, 0.0f, _currentRotation);
    }
}