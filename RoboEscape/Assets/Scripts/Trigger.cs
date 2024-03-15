using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Trigger : MonoBehaviour
{
    //public GameObject toggleableObject;
    [SerializeField] protected bool mIsActivated;
    [SerializeField] private Animator animator;
    private CableController _cableController;
    private SoundManager _soundManager;
    
    public bool isActivated => mIsActivated;

    private void Start() {
        _soundManager = GameObject.FindObjectOfType<SoundManager>();
        _cableController = this.GetComponent<CableController>();
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (!mIsActivated)
        {
            
            mIsActivated = true;
            _cableController.UpdateState(mIsActivated);
            animator.ResetTrigger("Up");
            animator.SetTrigger("Down");
            Debug.Log("BUTTON STAY");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (mIsActivated)
        {
            mIsActivated = false;
            _cableController.UpdateState(mIsActivated);
        }
        animator.ResetTrigger("Down");
        animator.SetTrigger("Up");
        Debug.Log("BUTTON UP");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!mIsActivated)
        {
            _soundManager.PlaySound("Button");
        }
        animator.ResetTrigger("Up");
        animator.SetTrigger("Down");
        Debug.Log("BUTTON DOWN");

    }
}
