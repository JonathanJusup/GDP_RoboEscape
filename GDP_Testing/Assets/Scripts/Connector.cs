using UnityEngine;
using UnityEngine.Serialization;

public class Connector : MonoBehaviour
{
    [SerializeField] private Transform fromGameObject;
    [SerializeField] private Transform toGameObject;

    //[SerializeField] private Material activatedMaterial;
    //[SerializeField] private Material deactivatedMaterial;
    
    private LineRenderer m_LineRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        m_LineRenderer = this.GetComponent<LineRenderer>();
        m_LineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        m_LineRenderer.SetPosition(0, fromGameObject.position);
        m_LineRenderer.SetPosition(1, toGameObject.position);
    }
}
