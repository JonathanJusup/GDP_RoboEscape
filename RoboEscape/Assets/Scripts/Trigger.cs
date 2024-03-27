using UnityEngine;

/**
 * Class for the button inside the game. Controls the events that occur, when a button has
 * been pressed.
 *
 * @authors Jonathan El Jusup (cgt104707), Florian Kern (cgt104661)
 */
public class Trigger : TriggerInterface {
    /// <summary>
    /// Button animator
    /// </summary>
    private Animator _buttonAnimator;
    
    
    /// <summary>
    /// Called before the first frame update. Sets SoundManager in order to play a sound when
    /// the button is pressed and sets the controller for the cables that connects the button.
    /// </summary>
    private void Start() {
        _buttonAnimator = this.GetComponentInChildren<Animator>();
        SoundManager = FindObjectOfType<SoundManager>();
        CableController = GetComponent<CableController>();
    }

    /// <summary>
    /// Called when an object collides with the button. Updates the state of cables/activates
    /// them and triggers an animation that indicates that the button has been pressed.
    /// </summary>
    /// <param name="other">Other colliding object</param>
    private void OnTriggerEnter(Collider other) {
        if (!isActivated) {
            isActivated = true;
            // Activating cables
            CableController.UpdateState(isActivated);
            // (Re)setting the triggers of the animation
            _buttonAnimator.ResetTrigger("Up");
            _buttonAnimator.SetTrigger("Down");

            // Playing sound of pushed button
            if (SoundManager) {
                SoundManager.PlaySound("Button");
            }
        }
    }

    /// <summary>
    /// Called when an object stays on top of the button. Updates the state of cables/activates
    /// them and triggers an animation that indicates that the button has been pressed.
    /// </summary>
    /// <param name="other">Other colliding object</param>
    private void OnTriggerStay(Collider other) {
        if (!isActivated) {
            isActivated = true;
            // Activating cables
            CableController.UpdateState(isActivated);
            // (Re)setting the triggers of the animation
            _buttonAnimator.ResetTrigger("Up");
            _buttonAnimator.SetTrigger("Down");
        }
    }

    /// <summary>
    /// Called when an object exits the collision with the button. Deactivates the cables
    /// and triggers an animation to revert the button to its original position.
    /// </summary>
    /// <param name="other">Other colliding object</param>
    private void OnTriggerExit(Collider other) {
        if (isActivated) {
            isActivated = false;
            // Deactivating cables
            CableController.UpdateState(isActivated);
        }

        // (Re)setting the triggers of the animation
        _buttonAnimator.ResetTrigger("Down");
        _buttonAnimator.SetTrigger("Up");
    }
}