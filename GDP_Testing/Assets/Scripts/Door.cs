using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isOpen = false;
    
    public void Open()
    {
        if (!isOpen)
        {
            animator.SetTrigger("OpenDoor");
            isOpen = true;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            animator.SetTrigger("CloseDoor");
            isOpen = false;    
        }
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }
    
}
