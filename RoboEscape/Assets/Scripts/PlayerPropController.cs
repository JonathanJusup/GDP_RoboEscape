using System.Collections;
using UnityEngine;

/// <summary>
/// Controller for the player props inside of the second level. PlayerProps
/// cannot move, but can be destroyed like the player. Upon destruction, spawns
/// robot parts on their position.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class PlayerPropController : MonoBehaviour {
    /** Collider of the player prop */
    private CapsuleCollider _collider;

    /** Robot parts spawner that spawns robot parts upon destruction of the player prop */
    private RobotPartsSpawner _robotPartsSpawner;

    /// <summary>
    /// Method is called before the first frame update.
    /// Initializes the collider of the player prop and the robot parts spawner.
    /// </summary>
    private void Start() {
        _collider = this.GetComponent<CapsuleCollider>();
        _robotPartsSpawner = this.GetComponent<RobotPartsSpawner>();
    }

    /// <summary>
    /// Destroys the player prop after a set delay using a coroutine.
    /// </summary>
    public void DieAfterDelay() {
        _collider = null;
        if (!_collider) {
            StartCoroutine(DieAfterDelayCoroutine());
        }
    }

    /// <summary>
    /// Destroys the player prop after a short delay.
    /// </summary>
    private IEnumerator DieAfterDelayCoroutine() {
        yield return new WaitForSeconds(0.5f);
        Die();
    }

    /// <summary>
    /// Destroys the player prop and spawns robot parts upon destruction.
    /// </summary>
    private void Die() {
        this.gameObject.SetActive(false);
        _robotPartsSpawner.SpawnParts(this.transform.position);
    }
}