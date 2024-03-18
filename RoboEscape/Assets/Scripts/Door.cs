using UnityEngine;

/**
 * Class that controls an animation for opening and closing the door
 * that leads to the next level.
 *
 * @authors Florian Kern (cgt104661), Jonathan Jusup (cgt104707)
 */
public class Door : MonoBehaviour
{
    /** Animator for the door animations */
    [SerializeField] private Animator animator;
    
    /** Flag that indicates if the door is open or not */
    private bool isOpen = false;

    
    
    /**
     * Triggers the animation that opens the door.
     */
    public void Open()
    {
        if (!isOpen)
        {
            // Setting trigger for door animation
            animator.SetTrigger("OpenDoor");
            isOpen = true;
        }
    }

    /**
     * Triggers the animation that closes the door.
     */
    public void Close()
    {
        if (isOpen)
        {
            // Setting trigger for door animation
            animator.SetTrigger("CloseDoor");
            isOpen = false;    
        }
    }

    /**
     * Getter for flag if door is open or not.
     */
    public bool GetIsOpen()
    {
        return isOpen;
    }
    
}
