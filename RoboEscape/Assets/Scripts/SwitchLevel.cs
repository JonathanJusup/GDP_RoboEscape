using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class for switching between levels.
 *
 * @authors Florian Kern (cgt104661), Jonathan Jusup (cgt104707)
 */
public class SwitchLevel : MonoBehaviour {
    /**
     * Update function is called once per frame.
     * Checks if a key to switch a level has been pressed or if the level wants to be restarted.
     */
    void Update() {
        if (!PauseMenuController.IsPaused) {
            //Check if the '1' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                //Load or switch to level 1
                SceneManager.LoadScene(1);
            }

            //Check if the '2' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                //Load or switch to level 2
                SceneManager.LoadScene(2);
            }

            //Check if the '3' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                //Load or switch to level 3
                SceneManager.LoadScene(3);
            }

            //Check if the '4' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                //Load or switch to level 4
                SceneManager.LoadScene(4);
            }

            //Check if the '5' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha5)) {
                //Load or switch to level 5
                SceneManager.LoadScene(5);
            }

            //Check if the '6' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha6)) {
                //Load or switch to level 6
                SceneManager.LoadScene(6);
            }

            // Check if the '7' key is pressed
            if (Input.GetKeyDown(KeyCode.Alpha7)) {
                //Load or switch to level 7
                SceneManager.LoadScene(7);
            }

            // Check if the 'R' key is pressed
            if (Input.GetKeyDown(KeyCode.R)) {
                //Restart level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}