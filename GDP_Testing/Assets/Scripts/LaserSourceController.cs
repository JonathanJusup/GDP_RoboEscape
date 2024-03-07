using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LaserSourceController : MonoBehaviour
{
    public Material material;
    private GameObject firstLaser;

    [SerializeField] private PressurePlate pressurePlate;

    private bool isControlled;
    private bool isActive = true;
    private ParticleSystem particleSystem;
    private Transform m_Transform;

    private void Start() {
        particleSystem = this.GetComponentInChildren<ParticleSystem>();
        this.m_Transform = this.transform;
        
        if (pressurePlate != null) {
            isControlled = true;
        }
        
    }

    // Update is called once per frame
    void Update() {
        if (isControlled) {
            isActive = pressurePlate.isPressed;   
        }

        Destroy(GameObject.Find(this.name + "LaserBeam"));
        if (isActive) {
            firstLaser = new GameObject(this.name + "LaserBeam");
            Laser laserComponent = firstLaser.AddComponent<Laser>();
            laserComponent.InitLaser(m_Transform.position, m_Transform.right, material, false, this);
        }

        if (isActive && !particleSystem.isPlaying) {
            particleSystem.Play();
            
            
        }
        else if (!isActive && particleSystem.isPlaying) {
            particleSystem.Stop();
        }
    }

    public void UpdateParticles(Vector3 position, Color color) {
        particleSystem.transform.position = position;
        
        ParticleSystem.MainModule settings = particleSystem.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(color, new Color(1.0f, 1.0f, 1.0f));
    }
}

