using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserSourceController : MonoBehaviour
{
    public Material material;

    [SerializeField] private Trigger _trigger;
    private bool isActive = true;
    
    private ParticleSystem particleSystem;
    private Transform transformComponent;
    
    [SerializeField] private Light pointLight;
    [SerializeField] private LaserPool _laserPool;
    [SerializeField] private Transform _laserContainer;

    private void Start() {
        particleSystem = this.GetComponentInChildren<ParticleSystem>();
        this.transformComponent = this.transform;
    }

    // Update is called once per frame
    void Update() {
        if (_trigger) {
            isActive = _trigger.isPressed;
            pointLight.enabled = isActive;
        }

        
        Destroy(GameObject.Find(this.name + "LaserBeam"));
        if (this.isActive) {
            GameObject firstLaser = new GameObject(this.name + "LaserBeam");
            Laser laserComponent = firstLaser.AddComponent<Laser>();
            laserComponent.InitLaser(transformComponent.position, transformComponent.right, material, false, this);
        }
        
        
        
        /*
        Reset();
        if (this.isActive) {
            GameObject initialLaser = _laserPool.GetLaser();
            if (!initialLaser) {
                return;
            }
            initialLaser.SetActive(true);
            
            initialLaser.transform.parent = _laserContainer;
            LaserBehaviour laserBehaviour = initialLaser.GetComponent<LaserBehaviour>();
            
            laserBehaviour.InitLaser(transformComponent.position, transformComponent.right, false, this);
        }
        */
        

        
        //Handle ParticleSystem
        if (isActive && !particleSystem.isPlaying) {
            particleSystem.Play();
        } else if (!isActive && particleSystem.isPlaying) {
            particleSystem.Stop();
        }
    }

    public void Reset() {
        foreach (Transform child in _laserContainer) {
            Destroy(child.gameObject);
            //_laserPool.ResetLaser(child.gameObject);
        }
    }

    public void UpdateParticles(Vector3 position, Color color) {
        particleSystem.transform.position = position;
        ParticleSystem.MainModule particleSystemMain = particleSystem.main;
        particleSystemMain.startColor = new ParticleSystem.MinMaxGradient(color, new Color(1.0f, 1.0f, 1.0f));

        pointLight.transform.position = position;
        pointLight.color = color;
    }

    public GameObject GetLaser() {
        return _laserPool.GetLaser();
    }

    public void ResetLaser(GameObject laser) {
        _laserPool.ResetLaser(laser);
    }
}

