using UnityEngine;

public class MeltingController : MonoBehaviour
{
    public float meltingDuration = 15.0f; // Zeitraum, über den das Schmelzen erfolgt
    private Vector3 initialScale;
    private bool isMelting = false;
    private Material m_Material;
    private float meltingTimer = 0f;




    private void Start()
    {
        initialScale = transform.localScale;
        m_Material = this.gameObject.GetComponent<Renderer>().material;
        m_Material.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (isMelting)
        {
            // Berechne den Schmelzfortschritt (0 bis 1)
            meltingTimer += Time.deltaTime;
            float meltProgress = Mathf.Clamp01(meltingTimer / meltingDuration);
            
            // Interpoliere die Skalierung, um den Schmelzeffekt zu erzeugen
            Vector3 newScale = Vector3.Lerp(initialScale, Vector3.zero, meltProgress);
            transform.localScale = newScale;
            if (meltProgress <= 0.01f)
            {
                FindObjectOfType<SoundManager>().PlaySound("Melt");
            }
            if (meltProgress == 1.0f)
            {
                Debug.Log("STOP MELTING");
                isMelting = !isMelting;
                GameObject block = GameObject.Find("MeltingBlock");
                block.SetActive(false);
                
            }
        }
    }
    
    // Methode, um den Schmelzvorgang zu starten
    public void StartMelting()
    {
        
        m_Material.EnableKeyword("_EMISSION");
        m_Material.SetColor("_EmissionColor", Color.red);
        m_Material.SetFloat("_EmissionScaleUI", 2.0f); 
        isMelting = true;
    }
    
}