using UnityEngine;

public class MeltingController : MonoBehaviour
{
    public float meltingDuration = 15.0f; // Zeitraum, über den das Schmelzen erfolgt
    private Vector3 initialScale;
    private bool isMelting = false;
    public Material targetMaterial;
    private float meltingTimer = 0f;




    private void Start()
    {
        initialScale = transform.localScale;
        targetMaterial.DisableKeyword("_EMISSION");
        
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
        targetMaterial.EnableKeyword("_EMISSION");
        targetMaterial.SetColor("_EmissionColor", Color.red);
        targetMaterial.SetFloat("_EmissionScaleUI", 2.0f); 
        isMelting = true;
    }
    
}