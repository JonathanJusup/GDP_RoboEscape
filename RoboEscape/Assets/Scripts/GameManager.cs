using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject eventSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        //If no EventSystem in Scene, instantiate one
        if (!transform.Find("EventSystem")) {
            Instantiate(eventSystem, this.transform);
        }
    }
}
