using System;
using UnityEngine;

public class shootLaser : MonoBehaviour
{
    public Material material;
    private GameObject firstLaser;

    [SerializeField] private PressurePlate pressurePlate;
    private bool isControlled;
    private bool isActive = true;

    private void Start()
    {
        if (pressurePlate != null)
        {
            isControlled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlled)
        {
            isActive = pressurePlate.isPressed;   
        }

        Destroy(GameObject.Find(this.name + "LaserBeam"));
        if (isActive)
        {
            firstLaser = new GameObject(this.name + "LaserBeam");
            Laser laserComponent = firstLaser.AddComponent<Laser>();
            laserComponent.InitLaser(transform.position, transform.right, material, false);
        }
    }
}

