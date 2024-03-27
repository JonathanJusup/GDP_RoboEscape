using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Class that loads the next level upon leaving the current one.
/// Plays an animation that acts as a loading animation.
///
/// @author Florian Kern (cgt104661)
/// </summary>
public class ExitLevel : MonoBehaviour {
    
    /** Transition animation */
    [SerializeField] private Animator transitionAnim;

    /** Duration of the transition */
    [SerializeField] private float transitionTime = 1f;
    
    
    
    /// <summary>
    /// Gets called when an object enters the area behind a door that leads to the next level.
    /// </summary>
    /// <param name="other"> The colliding object </param>
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
    
    
    
    
    /// <summary>
    /// Loads a level after an animation and a fixed time.
    /// </summary>
    /// <param name="index"> Index of the next level </param>
    /// <returns>TODO</returns>
    private IEnumerator LoadLevel(int index) {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        if (index == SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene("MenuScene");
        } else {
            SceneManager.LoadScene(index);
        }
    }
}