using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private float transitionTime = 1f;
    private Door doorScript;
    
    private void OnTriggerEnter(Collider other)
    {
        // TODO Debug entfernen, sobald man sicher ist, dass alles läuft wie es soll
        Debug.Log("VERLASSE LEVEL");
        doorScript = GameObject.Find("Door").GetComponent<Door>();
        if (other.gameObject.CompareTag("Player") && doorScript.GetIsOpen())
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // TODO ES MUSS EIGENTLICH +1 GERECHNET WERDEN, ÄNDERN, SOBALD TÜR AUCH IM INTRO IST
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadLevel(int index)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
    
}
