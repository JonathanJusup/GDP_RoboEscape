using UnityEngine;

/// <summary>
/// Class that controls the melting of a block.
///
/// @authors Prince Lare-Lantone (cgt104645), Florian Kern (cgt104661), Jonathan El Jusup (cgt104707)
/// </summary>
public class MeltingController : MonoBehaviour {
    /** Time frame in which the melting is executed */
    public float meltingDuration = 15.0f;

    /** Position, rotation and scale of the cube */
    private Transform _body;

    /** Initial scale of the cube */
    private Vector3 _initialScale;

    /** Flag that indicates if the cube is currently melting */
    private bool _isMelting = false;

    /** Material of the cube */
    private Material _material;

    /** Timer for the progress of the melting */
    private float _meltingTimer = 0f;

    private SoundManager _soundManager;


    /// <summary>
    /// Method is called before the first frame update.
    /// Sets relevant components such as the material of a cube.
    /// </summary>
    private void Start() {
        this._body = this.transform.GetChild(0);
        _initialScale = _body.localScale;
        _material = _body.gameObject.GetComponent<Renderer>().material;
        _material.DisableKeyword("_EMISSION");
        _soundManager = FindObjectOfType<SoundManager>();
    }


    /// <summary>
    /// Method is called every frame.
    /// Executes the melting of a cube over a set duration.
    /// </summary>
    private void Update() {
        if (_isMelting) {
            // Calculating melting progress (0 to 1)
            _meltingTimer += Time.deltaTime;
            float meltProgress = Mathf.Clamp01(_meltingTimer / meltingDuration);

            // Interpolating the scale in order to produce a melting effect
            Vector3 newScale = Vector3.Lerp(_initialScale, Vector3.zero, meltProgress);
            _body.localScale = newScale;
            if (meltProgress <= 0.01f) {
                _soundManager.PlaySound("Melt");
            }

            if (meltProgress >= 1.0f) {
                _isMelting = !_isMelting;
                this.gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// Starts the melting process.
    /// Sets a new material to the object.
    /// </summary>
    public void StartMelting() {
        _material.EnableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor", Color.red);
        _material.SetFloat("_EmissionScaleUI", 2.0f);
        _isMelting = true;
    }
}