using UnityEngine;

/// <summary>
/// Class that controls the melting of a block.
///
/// @authors Prince Lare-Lantone (cgt104645), Florian Kern (cgt104661), TODO Jonathan auch?
/// </summary>
public class MeltingController : MonoBehaviour
{
    /** Time frame in which the melting is executed */
    public float meltingDuration = 15.0f;
    
    /** Position, rotation and scale of the cube */
    private Transform body;
    
    /** Initial scale of the cube */
    private Vector3 initialScale;
    
    /** Flag that indicates if the cube is currently melting */
    private bool isMelting = false;
    
    /** Material of the cube */
    private Material m_Material;
    
    /** Timer for the progress of the melting */
    private float meltingTimer = 0f;

    
    /// <summary>
    /// Method is called before the first frame update.
    /// Sets relevant components such as the material of a cube.
    /// </summary>
    private void Start() {
        this.body = this.transform.GetChild(0);
        initialScale = body.localScale;
        m_Material = body.gameObject.GetComponent<Renderer>().material;
        m_Material.DisableKeyword("_EMISSION");
    }
    
    
    /// <summary>
    /// Method is called every frame.
    /// Executes the melting of a cube over a set duration.
    /// </summary>
    private void Update()
    {
        if (isMelting)
        {
            // Calculating melting progress (0 to 1)
            meltingTimer += Time.deltaTime;
            float meltProgress = Mathf.Clamp01(meltingTimer / meltingDuration);
            
            // Interpolating the scale in order to produce a melting effect
            Vector3 newScale = Vector3.Lerp(initialScale, Vector3.zero, meltProgress);
            body.localScale = newScale;
            if (meltProgress <= 0.01f)
            {
                FindObjectOfType<SoundManager>().PlaySound("Melt");
            }
            if (meltProgress >= 1.0f)
            {
                Debug.Log("STOP MELTING");
                isMelting = !isMelting;
                this.gameObject.SetActive(false);
                
            }
        }
    }
  
    
    /// <summary>
    /// Starts the melting process.
    /// Sets a new material to the object.
    /// </summary>
    public void StartMelting()
    {
        
        m_Material.EnableKeyword("_EMISSION");
        m_Material.SetColor("_EmissionColor", Color.red);
        m_Material.SetFloat("_EmissionScaleUI", 2.0f); 
        isMelting = true;
    }
    
}