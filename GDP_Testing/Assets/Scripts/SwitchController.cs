using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private bool m_IsActive;
    [SerializeField] private float initTimer = 1.0f;
    private float m_CurrentTimer;
    
    //[SerializableField] private Door door;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTimer = initTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsActive)
        {
            m_CurrentTimer -= Time.deltaTime;
            if (m_CurrentTimer <= 0.0f)
            {
                m_IsActive = false;
            }
            
            //door.open();
        }
        else
        {
            //door.close();
        }
    }

    public void ActivateSwitch()
    {
        m_IsActive = true;
        m_CurrentTimer = initTimer;
    }
}
