using UnityEngine;

/// <summary>
/// TODO
///
/// @author Jonathan Jusup (cgt104707)
/// </summary>
public class Connector : MonoBehaviour
{
    /** TODO */
    [SerializeField] private Transform fromGameObject;
    
    /** TODO */
    [SerializeField] private Transform toGameObject;
    
    /** TODO */
    private LineRenderer m_LineRenderer;
    
    /// <summary>
    /// Method gets called before the first frame update.
    /// Sets the line renderer component and its positionCount.
    /// </summary>
    void Start()
    {
        m_LineRenderer = this.GetComponent<LineRenderer>();
        m_LineRenderer.positionCount = 2;
    }

    /// <summary>
    /// Method gets called once per frame.
    /// Sets the position of the line renderer at both positions.
    /// </summary>
    void Update()
    {
        m_LineRenderer.SetPosition(0, fromGameObject.position);
        m_LineRenderer.SetPosition(1, toGameObject.position);
    }
}
