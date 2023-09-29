using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropController : MonoBehaviour
{
    private CapsuleCollider collider;
    private Material material;

    private void Start()
    {
        collider = this.GetComponent<CapsuleCollider>();
        material = this.gameObject.GetComponent<Renderer>().material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        { 
            Die();
        }
    }

    public void DieAfterDelay()
    {
        collider = null;
        if (!collider)
        {
            material.color = Color.red;
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
