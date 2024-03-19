using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        //TODO: Not everything is ground -> Add condition
        playerController.SetIsGrounded(true);
    }


}