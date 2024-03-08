using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class trigger : MonoBehaviour
{
    //public GameObject toggleableObject;
    [SerializeField] private bool m_IsPressed;
    private CableController cableController;

    public bool isPressed => m_IsPressed;

    private void Start() {
        cableController = this.GetComponent<CableController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!m_IsPressed)
        {
            
            m_IsPressed = true;
            cableController.UpdateState(m_IsPressed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_IsPressed)
        {
            m_IsPressed = false;
            cableController.UpdateState(m_IsPressed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_IsPressed)
        {
            FindObjectOfType<SoundManager>().PlaySound("Button");
        }
        
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        //TODO: Add additional trigger conditions eg. Only player, boxes can trigger
        if (!m_IsPressed)
        {
            m_IsPressed = true;
            toggleObject(m_IsPressed);
            Debug.Log("[ENTER] Pressed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //TODO: Add additional trigger conditions eg. Only player, boxes can trigger
        if (m_IsPressed)
        {
            m_IsPressed = false;
            toggleObject(m_IsPressed);
            Debug.Log("[EXIT] Pressed");
        }
    }

    private void toggleObject(bool state)
    {
        if (!toggleableObject)
        {
            Debug.Log("[ERROR] ToggleableObject is null");
            //throw new System.NullReferenceException("[ERROR] Toggleable Object is null");
        }

        BaseToggleComponent toggleComponent = toggleableObject.GetComponent<BaseToggleComponent>();
        if (!toggleComponent)
        {
            Debug.Log("[ERROR] ToggleableObject has no toggleComponent");
        }

        toggleComponent.Toggle(state);
    }
    */
}
