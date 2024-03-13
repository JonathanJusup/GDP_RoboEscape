using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Trigger : MonoBehaviour
{
    //public GameObject toggleableObject;
    [SerializeField] private bool m_IsPressed;
    [SerializeField] private Animator animator;
    private CableController _cableController;
    private SoundManager _soundManager;
    
    public bool isPressed => m_IsPressed;

    private void Start() {
        _cableController = this.GetComponent<CableController>();
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (!m_IsPressed)
        {
            
            m_IsPressed = true;
            _cableController.UpdateState(m_IsPressed);
            animator.ResetTrigger("Up");
            animator.SetTrigger("Down");
            Debug.Log("BUTTON STAY");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_IsPressed)
        {
            m_IsPressed = false;
            _cableController.UpdateState(m_IsPressed);
        }
        animator.ResetTrigger("Down");
        animator.SetTrigger("Up");
        Debug.Log("BUTTON UP");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_IsPressed)
        {
            FindObjectOfType<SoundManager>().PlaySound("Button");
        }
        animator.ResetTrigger("Up");
        animator.SetTrigger("Down");
        Debug.Log("BUTTON DOWN");

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
