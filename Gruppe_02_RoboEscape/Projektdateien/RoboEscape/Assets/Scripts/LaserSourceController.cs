using UnityEngine;

/// <summary>
/// Controller for the source of the laser.
///
/// @author Jonathan El Jusup (cgt104707), Florian Kern (cgt104661)
/// </summary>
public class LaserSourceController : MonoBehaviour {
    /** Material of the source */
    public Material material;

    /** Reference to the trigger interface */
    [SerializeField] private Trigger trigger;

    /** Bool if the laser will be deadly or not */
    [SerializeField] private bool isDeadly = false;

    /** Bool if the laser will be active or not */
    private bool _isActive = true;

    /** Particle system for the laser */
    private ParticleSystem _particleSystem;

    /** Transform of the laser source */
    private Transform _transform;

    /** The sound manager */
    private SoundManager _soundManager;

    /** Point light for the laser */
    [SerializeField] private Light pointLight;

    /** Transform of the laser */
    [SerializeField] private Transform laserContainer;

    /// <summary>
    /// Method is called before the first frame update.
    /// Assigns particle system, laser source transform and the soundManager.
    /// </summary>
    private void Start() {
        _particleSystem = this.GetComponentInChildren<ParticleSystem>();
        _soundManager = FindObjectOfType<SoundManager>();
        this._transform = this.transform;
    }


    /// <summary>
    /// Method gets called once per frame.
    /// Shoots and destroys a new laser every update from its position to
    /// account for situational changes. Plays a humming sound when the laser
    /// is active or pauses the sound if it is inactive. Plays the particleSystem
    /// if active.
    /// </summary>
    void Update() {
        if (trigger) {
            _isActive = trigger.getIsActivated;
            pointLight.enabled = _isActive;
        }

        // Destroying each child in the container
        foreach (Transform child in laserContainer) {
            Destroy(child.gameObject);
        }

        // Initializing laser if source is active
        if (this._isActive) {
            GameObject firstLaser = new GameObject(this.name + "LaserBeam");
            Laser laserComponent = firstLaser.AddComponent<Laser>();
            laserComponent.InitLaser(_transform.position, _transform.right, laserContainer, material, this.isDeadly,
                this);

            // Playing or pausing sound
            if (_soundManager) {
                _soundManager.PlaySound("Laser");
            }
        } else {
            if (_soundManager) {
                _soundManager.PauseSound("Laser");
            }
        }

        //Handle ParticleSystem
        if (_isActive && !_particleSystem.isPlaying) {
            _particleSystem.Play();
        } else if (!_isActive && _particleSystem.isPlaying) {
            _particleSystem.Stop();
        }
    }

    /// <summary>
    /// Updates position and color of the particles of the particleSystem and the pointLight.
    /// </summary>
    /// <param name="position">Position particles and pointLight</param>
    /// <param name="color">Color of particles and pointLight</param>
    public void UpdateParticles(Vector3 position, Color color) {
        //Setting new position of particles
        _particleSystem.transform.position = position;
        ParticleSystem.MainModule particleSystemMain = _particleSystem.main;

        //Setting new start color of particles
        particleSystemMain.startColor = new ParticleSystem.MinMaxGradient(color, new Color(1.0f, 1.0f, 1.0f));

        //Setting new position and color of the pointLight
        pointLight.transform.position = position;
        pointLight.color = color;
    }
}