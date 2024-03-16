using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LaserSourceController : MonoBehaviour {
    public Material material;

    [SerializeField] private TriggerInterface trigger;
    [SerializeField] private bool isDeadly = false;

    private bool _isActive = true;

    private ParticleSystem _particleSystem;
    private Transform _transform;
    private SoundManager _soundManager;
    [SerializeField] private Light pointLight;
    [SerializeField] private Transform laserContainer;

    private void Start() {
        _particleSystem = this.GetComponentInChildren<ParticleSystem>();
        _soundManager = FindObjectOfType<SoundManager>();
        this._transform = this.transform;
    }

    // Update is called once per frame
    void Update() {
        if (trigger) {
            _isActive = trigger.getIsActivated;
            pointLight.enabled = _isActive;
        }


        //Destroy(GameObject.Find(this.name + "LaserBeam"));
        foreach (Transform child in laserContainer) {
            Destroy(child.gameObject);
        }
        
        if (this._isActive) {
            GameObject firstLaser = new GameObject(this.name + "LaserBeam");
            Laser laserComponent = firstLaser.AddComponent<Laser>();
            laserComponent.InitLaser(_transform.position, _transform.right, laserContainer, material, this.isDeadly, this);
            
            if (_soundManager) {
                _soundManager.PlaySound("Laser");
            }
        }
        else {
            if (_soundManager) {
                _soundManager.PauseSound("Laser");
            }
        }

        //Handle ParticleSystem
        if (_isActive && !_particleSystem.isPlaying) {
            _particleSystem.Play();
        }
        else if (!_isActive && _particleSystem.isPlaying) {
            _particleSystem.Stop();
        }
    }

    public void UpdateParticles(Vector3 position, Color color) {
        _particleSystem.transform.position = position;
        ParticleSystem.MainModule particleSystemMain = _particleSystem.main;
        particleSystemMain.startColor = new ParticleSystem.MinMaxGradient(color, new Color(1.0f, 1.0f, 1.0f));

        pointLight.transform.position = position;
        pointLight.color = color;
    }
}