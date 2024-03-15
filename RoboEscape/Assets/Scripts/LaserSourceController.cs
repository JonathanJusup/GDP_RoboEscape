using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserSourceController : MonoBehaviour
{
    public Material material;

    [SerializeField] private Trigger trigger;
    [SerializeField] private bool isDeadly = false;
    
    private bool _isActive = true;
    
    private ParticleSystem _particleSystem;
    private Transform _transformComponent;
    private SoundManager _soundManager;
    [SerializeField] private Light pointLight;
    [SerializeField] private LaserPool laserPool;
    [SerializeField] private Transform laserContainer;

    private void Start() {
        _particleSystem = this.GetComponentInChildren<ParticleSystem>();
        _soundManager = FindObjectOfType<SoundManager>();
        this._transformComponent = this.transform;
    }

    // Update is called once per frame
    void Update() {
        if (trigger) {
            _isActive = trigger.isActivated;
            pointLight.enabled = _isActive;
        }

        
        Destroy(GameObject.Find(this.name + "LaserBeam"));
        if (this._isActive) {
            GameObject firstLaser = new GameObject(this.name + "LaserBeam");
            Laser laserComponent = firstLaser.AddComponent<Laser>();
            laserComponent.InitLaser(_transformComponent.position, _transformComponent.right, material, this.isDeadly, this);
            if (_soundManager)
            {
                _soundManager.PlaySound("Laser");
            }
            
            
        }
        else
        {
            _soundManager.PauseSound("Laser");
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
            
            laserBehaviour.InitLaser(transformComponent.position, transformComponent.right, this.isDeadly, this);
        }
        */
        

        
        //Handle ParticleSystem
        if (_isActive && !_particleSystem.isPlaying) {
            _particleSystem.Play();
        } else if (!_isActive && _particleSystem.isPlaying) {
            _particleSystem.Stop();
        }
    }

    public void Reset() {
        foreach (Transform child in laserContainer) {
            Destroy(child.gameObject);
            //_laserPool.ResetLaser(child.gameObject);
        }
    }

    public void UpdateParticles(Vector3 position, Color color) {
        _particleSystem.transform.position = position;
        ParticleSystem.MainModule particleSystemMain = _particleSystem.main;
        particleSystemMain.startColor = new ParticleSystem.MinMaxGradient(color, new Color(1.0f, 1.0f, 1.0f));

        pointLight.transform.position = position;
        pointLight.color = color;
    }

    public GameObject GetLaser() {
        return laserPool.GetLaser();
    }

    public void ResetLaser(GameObject laser) {
        laserPool.ResetLaser(laser);
    }
    
    
}

