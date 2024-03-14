using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RobotPartsSpawner : MonoBehaviour
{
    //public GameObject cube;
    [SerializeField] private int numberOfScraps = 10;
    [SerializeField] private List<GameObject> scraps;
    
    [SerializeField] private List<GameObject> bodyParts;
    [SerializeField] private float force = 1.0f;

    public void SpawnParts(Vector3 pos)
    {
        for (int i = 0; i < numberOfScraps; i++) {
            int objectIdx = Random.Range(0, scraps.Count - 1);
            GameObject scrap = Instantiate(scraps[objectIdx], pos, Quaternion.identity);
            Rigidbody rb = scrap.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            rb.AddForce(direction * force, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, Random.Range(0.0f, 360.0f)), ForceMode.Impulse);
        }
        
        for (int i = 0; i < numberOfScraps; i++) {
            GameObject scrap = Instantiate(bodyParts[i], pos, Quaternion.identity);
            Rigidbody rb = scrap.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            rb.AddForce(direction * force, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, Random.Range(0.0f, 360.0f)), ForceMode.Impulse);
        }
    }
    
}
