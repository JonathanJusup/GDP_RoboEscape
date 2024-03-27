using UnityEngine;

/// <summary>
/// Class for the button inside the game. Controls the events that occur, when a button has been pressed.
///
/// @authors Jonathan Jusup (cgt104707), Florian Kern (cgt104661)
/// </summary>


public class Trigger : TriggerInterface {
    
    // Animator for the button
    [SerializeField] private Animator buttonAnimator;
    
    /// <summary>
    /// Method is called before the first frame update.
    /// Sets the soundmanager in order to play a sound when the button is pressed and sets the controller for the
    /// cables that surround the button.
    /// </summary>
    private void Start() {
        SoundManager = FindObjectOfType<SoundManager>();
        CableController = GetComponent<CableController>();
    }
    
    
    /// <summary>
    /// Gets called when an object collides with the button.
    /// Updates the state of cables/activates them and triggers an animation that indicates that the button has been
    /// pressed.
    /// </summary>
    /// <param name="other"> The colliding object </param>
    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated)
        {
            isActivated = true;
            // Activating cables
            CableController.UpdateState(isActivated);
            // (Re)setting the triggers of the animation
            buttonAnimator.ResetTrigger("Up");
            buttonAnimator.SetTrigger("Down");

            // Playing sound of pushed button
            if (SoundManager) {
                SoundManager.PlaySound("Button");
            }
        }
    }
    
    
    /// <summary>
    /// Gets called when an object stays on top of the button.
    /// Updates the state of cables/activates them and triggers an animation that indicates that the button has been
    /// pressed.
    /// </summary>
    /// <param name="other"> The colliding object </param>
    private void OnTriggerStay(Collider other)
    {
        if (!isActivated)
        {
            isActivated = true;
            // Activating cables
            CableController.UpdateState(isActivated);
            // (Re)setting the triggers of the animation
            buttonAnimator.ResetTrigger("Up");
            buttonAnimator.SetTrigger("Down");
        }
    }
    
    /// <summary>
    /// Gets called when an object exits the collision with the button.
    /// Deactivates the cables and triggers an animation to revert the button to its original position.
    /// </summary>
    /// <param name="other"> The colliding object </param>
   private void OnTriggerExit(Collider other)
    {
        if (isActivated)
        {
            isActivated = false;
            // Deactivating cables
            CableController.UpdateState(isActivated);
        }
        // (Re)setting the triggers of the animation
        buttonAnimator.ResetTrigger("Down");
        buttonAnimator.SetTrigger("Up");

    }


}
