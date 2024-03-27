using UnityEngine;

/// <summary>
/// Game manager for the event system.
///
/// @author Jonathan Jusup (cgt104707)
/// </summary>
public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject eventSystem;
    
    /// <summary>
    /// Method gets called before the first frame update.
    /// Instantiates an event system if it does not exist.
    /// </summary>
    void Start()
    {
        //If no EventSystem in Scene, instantiate one
        if (!transform.Find("EventSystem")) {
            Instantiate(eventSystem, this.transform);
        }
    }
}
