using DefaultNamespace;
using UnityEngine;


/// <summary>
/// Class for the mirror. Inherits from BaseToggleComponent and overrides the toggle method.
///
/// @author Jonathan Jusup (cgt104707)
/// </summary>
public class Mirror : BaseToggleComponent
{
    /** Flag if the mirror should rotate or not */
    private bool m_DoRotate;
    
    /** Speed of the rotation */
    [SerializeField] private float rotationSpeed = 1.0f;
    
    /// <summary>
    /// Toggles a state as active or inactive.
    /// </summary>
    /// <param name="state"> State of a property </param>
    public override void Toggle(bool state)
    {
        m_DoRotate = state;
    }

    /// <summary>
    /// Method gets called once per frame.
    /// Rotates the mirror if it is supposed to rotate.
    /// </summary>
    private void Update()
    {
        if (m_DoRotate)
        {
            transform.Rotate(Vector3.forward, rotationSpeed);
        }
    }
}
