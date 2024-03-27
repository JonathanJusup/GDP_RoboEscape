using UnityEngine;

/// <summary>
/// Controller for the source of the laser.
///
/// @author Jonathan Jusup (cgt104707), Florian Kern (cgt104661)
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
    /// Sets the particle system, the laser source transform and the sound manager.
    /// </summary>
    private void Start() {
        _particleSystem = this.GetComponentInChildren<ParticleSystem>();
        _soundManager = FindObjectOfType<SoundManager>();
        this._transform = this.transform;
    }

    
    /// <summary>
    /// Method gets called once per frame.
    /// Plays a humming sound when the laser is active or pauses the sound if it is inactive.
    /// Adds a Laser-object as a component to the source and initializes it, when the source is active.
    /// Activates the particle system upon activation of the laser.
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
            laserComponent.InitLaser(_transform.position, _transform.right, laserContainer, material, this.isDeadly, this);
            
            // Playing or pausing sound
            if (_soundManager) {
                _soundManager.PlaySound("Laser");
            }
        }
        else {
            if (_soundManager) {
                _soundManager.PauseSound("Laser");
            }
        }

        //Handle ParticleSystem
        if (_isActive && !_particleSystem.isPlaying) {
            _particleSystem.Play();
        }
        else if (!_isActive && _particleSystem.isPlaying) {
            _particleSystem.Stop();
        }
    }

    /// <summary>
    /// Updates the position and color of the particles of the particle system and the pointlight.
    /// </summary>
    /// <param name="position"> Position of the particles and the pointlight</param>
    /// <param name="color"> Color of the particles and the pointlight </param>
    public void UpdateParticles(Vector3 position, Color color) {
        // Setting new position of particles
        _particleSystem.transform.position = position;
        ParticleSystem.MainModule particleSystemMain = _particleSystem.main;
        // Setting new start color of particles
        particleSystemMain.startColor = new ParticleSystem.MinMaxGradient(color, new Color(1.0f, 1.0f, 1.0f));

        // Settingn new position and color of the pointlight
        pointLight.transform.position = position;
        pointLight.color = color;
    }
}