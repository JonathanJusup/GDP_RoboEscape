using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the robot parts and scraps that get spawned when
/// the player or a robot prop dies.
///
/// @author Jonathan El Jusup (cgt104707)
/// </summary>
public class RobotPartsSpawner : MonoBehaviour {
    /** Number of spawning parts */
    [SerializeField] private int numberOfScraps = 10;

    /** List for the spawning scrap */
    [SerializeField] private List<GameObject> scraps;

    /** List for the spawning parts of the robot */
    [SerializeField] private List<GameObject> bodyParts;

    /** Force at which the robot parts and scrap fly away from the position where the player died */
    [SerializeField] private float force = 1.0f;

    /// <summary>
    /// Spawns the scrap and player parts when the player dies.
    /// Iterates through both lists, spawns the objects inside them and apply a force to their rigidbodies
    /// that sends them flying through the scene in a random direction.
    /// </summary>
    /// <param name="pos">Position of the player</param>
    public void SpawnParts(Vector3 pos) {
        // Iterating through scrap list
        for (int i = 0; i < numberOfScraps; i++) {
            // Getting random scrap
            int objectIdx = Random.Range(0, scraps.Count - 1);

            // Spawning object
            GameObject scrap = Instantiate(scraps[objectIdx], pos, Quaternion.identity);
            Rigidbody rb = scrap.GetComponent<Rigidbody>();

            // Getting random direction 
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

            // Applying force and torque to the rigidbody of the object
            rb.AddForce(direction * force, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, Random.Range(0.0f, 360.0f)), ForceMode.Impulse);
        }

        // Iterating through body parts list
        for (int i = 0; i < numberOfScraps; i++) {
            // Spawning object
            GameObject scrap = Instantiate(bodyParts[i], pos, Quaternion.identity);
            Rigidbody rb = scrap.GetComponent<Rigidbody>();

            // Getting random direction
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

            // Applying force and torque to the rigidbody of the object
            rb.AddForce(direction * force, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, Random.Range(-0.5f, 0.5f)), ForceMode.Impulse);
        }
    }
}