using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private int levelIdx = 0;
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
        StartCoroutine(LoadLevel(levelIdx));
    }

    IEnumerator LoadLevel(int index)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
    
}
