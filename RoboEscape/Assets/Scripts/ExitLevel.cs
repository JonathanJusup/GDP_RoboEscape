using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class that loads the next level upon leaving the current one.
 * Plays an animation that acts as a loading animation.
 *
 * @author Florian Kern (cgt104661)
 */
public class ExitLevel : MonoBehaviour
{
    /** Transition animation */
    [SerializeField] private Animator transitionAnim;
    
    /** Duration of the transition */
    [SerializeField] private float transitionTime = 1f;

    /**
     * Gets called when an object enters the area behind a door that leads to the next level. 
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextLevel();    
        }
        
    }

    /**
     * Loads the next level using a coroutine.
     */
    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /**
     * Loads a level after an animation and a fixed time.
     */
    IEnumerator LoadLevel(int index)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        if (index == SceneManager.sceneCountInBuildSettings )
        {
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            SceneManager.LoadScene(index);
        }
        
    }
    
}
