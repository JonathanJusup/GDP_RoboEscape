using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for switching between levels and restarting
///
/// @authors Florian Kern (cgt104661), Jonathan El Jusup (cgt104707)
/// </summary>
public class SwitchLevel : MonoBehaviour {
    /// <summary>
    /// Update function is called once per frame.
    /// Checks if a key to switch a level has been pressed or if the level wants to be restarted.
    /// </summary>
    void Update() {
        if (!PauseMenuController.IsPaused) {
            //If '1' key is pressed, load level 1
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                SceneManager.LoadScene(1);
            }

            //If '2' key is pressed, load level 2
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                SceneManager.LoadScene(2);
            }

            //If '3' key is pressed, load level 3
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                SceneManager.LoadScene(3);
            }

            //If '4' key is pressed, load level 4
            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                SceneManager.LoadScene(4);
            }

            //If '5' key is pressed, load level 5
            if (Input.GetKeyDown(KeyCode.Alpha5)) {
                SceneManager.LoadScene(5);
            }

            //If '6' key is pressed, load level 6
            if (Input.GetKeyDown(KeyCode.Alpha6)) {
                SceneManager.LoadScene(6);
            }

            //If '7' key is pressed, load level 7
            if (Input.GetKeyDown(KeyCode.Alpha7)) {
                SceneManager.LoadScene(7);
            }

            //If '8' key is pressed, load level 8
            if (Input.GetKeyDown(KeyCode.Alpha8)) {
                SceneManager.LoadScene(8);
            }

            //If 'R' key is pressed, restart level
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}