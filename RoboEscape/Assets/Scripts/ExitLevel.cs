using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class that simulates an effect when leaving a level.
 *
 * @author Florian Kern (cgt104661)
 */
public class ExitLevel : MonoBehaviour
{
    /** Transition animation for the next level */
    [SerializeField] private Animator transitionAnim;
    
    /** Duration of the transition */
    [SerializeField] private float transitionTime = 1f;
    
    /** The level index TODO DELETE AND USE CURRENT LEVEL INDEX + 1 */
    [SerializeField] private int levelIdx = 0;

    /**
     * Gets called when an object enters the area behind a door that leads to the next level. 
     */
    private void OnTriggerEnter(Collider other)
    {
        LoadNextLevel();
    }

    /**
     * Loads the next level using a coroutine.
     */
    private void LoadNextLevel()
    {
        // TODO aktuellen Level-Index + 1 verwenden, dann entfällt auch levelIdx
        StartCoroutine(LoadLevel(levelIdx));
    }

    /**
     * Loads a level after an animation and a fixed time.
     */
    IEnumerator LoadLevel(int index)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
    
}
