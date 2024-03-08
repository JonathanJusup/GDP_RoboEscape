using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;
    public int numberOfCubes = 10;
    [SerializeField] private float force = 1.0f;

    public void SpawnCubes(Vector3 pos)
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            GameObject cube = Instantiate(this.cube, pos, Quaternion.identity);
            Rigidbody rb = cube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //float force = Random.Range(0.2f, 0.3f);
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
                rb.AddForce(direction * force, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("NO RIGIDBODY");
            }
        }
    }
    
}
