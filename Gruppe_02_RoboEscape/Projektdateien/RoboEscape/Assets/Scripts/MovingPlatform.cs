using UnityEngine;

/// <summary>
/// Class that controls the moving platforms. A platform constantly
/// moved between two defined positions. The player can stand on it
/// and moves relatively to its position.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class MovingPlatform : MonoBehaviour {
    /** Target position */
    [SerializeField] private Vector3 targetPos;

    /**Platform movement speed */
    [SerializeField] private float speed;

    /** Player to attach on platform transform */
    private Transform _player;

    /** Starting position */
    private Vector3 _startPos;

    /** Flag, if platform is moving to target or back to start */
    private bool _isMovingToTarget = true;

    /** Current position */
    private Vector3 _currentPos;


    /// <summary>
    /// Method gets called before the first frame update.
    /// Sets the starting position and current position of the platform and the player.
    /// </summary>
    void Start() {
        this._startPos = this.transform.position;
        this._currentPos = this._startPos;
        this._player = GameObject.Find("Player").transform;
    }

    /// <summary>
    /// Method gets called once per frame.
    /// Moves the platform to its target position as long as it has not reached that position.
    /// Upon reaching its target, it moves back to the starting position.
    /// </summary>
    void Update() {
        if (_isMovingToTarget) {
            //Moving to target, until reached
            _currentPos = Vector3.MoveTowards(_currentPos, targetPos, Time.deltaTime * speed);
            if (_currentPos == targetPos) {
                _isMovingToTarget = false;
            }
        } else {
            //Moving back to start, until reached
            _currentPos = Vector3.MoveTowards(_currentPos, _startPos, Time.deltaTime * speed);
            if (_currentPos == _startPos) {
                _isMovingToTarget = true;
            }
        }

        transform.position = _currentPos;
    }


    /// <summary>
    /// Sticks player to moving platform transform, when player stands on it.
    /// Player still can move freely on the platform.
    /// </summary>
    /// <param name="other">Player collider</param>
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _player.transform.parent = this.transform;
        }
    }


    /// <summary>
    /// Resets player parent transform, so its no longer based on position
    /// of moving platform.
    /// </summary>
    /// <param name="other">Player collider</param>
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            _player.transform.parent = null;
        }
    }
}