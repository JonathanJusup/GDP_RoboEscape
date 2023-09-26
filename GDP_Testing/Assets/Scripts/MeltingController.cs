using UnityEngine;

public class MeltingController : MonoBehaviour
{
    
    private bool isColliding = false;
    public Material targetMaterial;

    private void Start()
    {
    }

    private void Update()
    {
        if (isColliding)
        {
            Vector3 scale = transform.localScale;
            // Hier ändern wir die Skalierung des Blocks
            Vector3 newScale = new Vector3(scale.x, scale.y * 0.5f, scale.z);
            transform.localScale = newScale;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Überprüfe, ob die Kollision mit dem gewünschten Objekt stattgefunden hat
        if (collision.gameObject.CompareTag("Meltable"))
        {
            Debug.Log("MELTING");
            isColliding = true;
            targetMaterial.EnableKeyword("_EMISSION");
            targetMaterial.SetColor("_EmissionColor", Color.red);
            targetMaterial.SetFloat("_EmissionScaleUI", 2.0f);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Überprüfe, ob die Kollision mit dem gewünschten Objekt beendet wurde
        if (collision.gameObject.CompareTag("Meltable"))
        {
            isColliding = false;
            targetMaterial.DisableKeyword("_Emission");
        }
    }
}