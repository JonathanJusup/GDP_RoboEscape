using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;
    public int numberOfCubes = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCubes(Vector3 pos)
    {
        // TODO Kraft verringern
        for (int i = 0; i < numberOfCubes; i++)
        {
            GameObject cube = Instantiate(this.cube, pos, Quaternion.identity);
            Rigidbody rb = this.cube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float force = Random.Range(0.2f, 0.3f);
                Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f))
                    .normalized;
                rb.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }
    
}
