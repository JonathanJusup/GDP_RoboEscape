using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserSourceController : MonoBehaviour
{
    public Material material;
    private GameObject firstLaser;

    [FormerlySerializedAs("pressurePlate")] [SerializeField] private trigger trigger;

    private bool isControlled;
    private bool isActive = true;
    private ParticleSystem particleSystem;
    private Transform m_Transform;
    [SerializeField] private Light pointLight;

    private void Start() {
        particleSystem = this.GetComponentInChildren<ParticleSystem>();
        this.m_Transform = this.transform;
        
        if (trigger != null) {
            isControlled = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (isControlled) {
            isActive = trigger.isPressed;
            pointLight.enabled = isActive;
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
        ParticleSystem.MainModule particleSystemMain = particleSystem.main;
        particleSystemMain.startColor = new ParticleSystem.MinMaxGradient(color, new Color(1.0f, 1.0f, 1.0f));

        pointLight.transform.position = position;
        pointLight.color = color;
    }
}

