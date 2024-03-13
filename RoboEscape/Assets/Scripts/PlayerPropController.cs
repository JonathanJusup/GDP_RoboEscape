using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropController : MonoBehaviour
{
    private CapsuleCollider collider;

    private void Start()
    {
        collider = this.GetComponent<CapsuleCollider>();
    }

    public void DieAfterDelay()
    {
        collider = null;
        if (!collider)
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
        CubeSpawner cubeSpawner = GameObject.Find("CubeSpawn").GetComponent<CubeSpawner>();
        cubeSpawner.SpawnCubes(this.transform.position);
    }
}
