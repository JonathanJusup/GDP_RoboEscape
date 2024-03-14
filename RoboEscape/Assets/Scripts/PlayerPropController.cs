using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropController : MonoBehaviour
{
    private CapsuleCollider _collider;
    private RobotPartsSpawner _robotPartsSpawner;

    private void Start()
    {
        _collider = this.GetComponent<CapsuleCollider>();
        _robotPartsSpawner = this.GetComponent<RobotPartsSpawner>();
    }

    public void DieAfterDelay()
    {
        _collider = null;
        if (!_collider)
        {
            StartCoroutine(DieAfterDelayCoroutine());
        }
    }

    private IEnumerator DieAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Die();
    }
    
    private void Die()
    {
        this.gameObject.SetActive(false);
        _robotPartsSpawner.SpawnParts(this.transform.position);
    }
}
