using UnityEngine;


/// <summary>
/// Class that controls a simple door and its animation for opening
/// and closing it.
///
/// @authors Florian Kern (cgt104661), Jonathan El Jusup (cgt104707)
/// </summary>
public class Door : MonoBehaviour {
    /** Animator for the door animations */
    [SerializeField] private Animator animator;

    /** Flag that indicates if the door is open or not */
    private bool _isOpen = false;


    /// <summary>
    /// Triggers opening animation of the opens and update its state.
    /// </summary>
    public void Open() {
        if (!_isOpen) {
            // Setting trigger for door animation
            animator.SetTrigger("OpenDoor");
            _isOpen = true;
        }
    }


    /// <summary>
    /// Triggers closing animation of the door and updates its state.
    /// </summary>
    public void Close() {
        if (_isOpen) {
            // Setting trigger for door animation
            animator.SetTrigger("CloseDoor");
            _isOpen = false;
        }
    }


    /// <summary>
    /// Getter for current state
    /// </summary>
    /// <returns>Current state of door</returns>
    public bool GetIsOpen() {
        return _isOpen;
    }
}