using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlatform : MonoBehaviour {
    [SerializeField] private Vector3 targetPos;         //Target position
    [SerializeField] private float speed;               //Platform movement speed
    
    private Transform _player;                          //Player to attach on platform transform 
    private Vector3 _startPos;                          //Starting position
    private bool _isMovingToTarget = true;              //Flag, if platform is moving to target or back to start
    private Vector3 _currentPos;                        //Current position
    
    // Start is called before the first frame update
    void Start() {
        this._startPos = this.transform.position;
        this._currentPos = this._startPos;
        this._player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMovingToTarget) {
            //Moving to target, until reached
            _currentPos = Vector3.MoveTowards(_currentPos, targetPos, Time.deltaTime * speed);
            if (_currentPos == targetPos) {
                _isMovingToTarget = false;
            }
        }
        else {
            //Moving back to start, until reached
            _currentPos = Vector3.MoveTowards(_currentPos, _startPos, Time.deltaTime * speed);
            if (_currentPos == _startPos) {
                _isMovingToTarget = true;
            }
        }

        transform.position = _currentPos;
    }

    /**
     * Sticks player to moving platform transform, when player stands on it.
     * Player still can move freely on the platform.
     *
     * @param other player collider
     */
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Something entered moving platform");
        if (other.CompareTag("Player")) {
            _player.transform.parent = this.transform;
        }
    }

    /**
     * Resets player parent transform, so its no longer based on position
     * of moving platform
     *
     * @param other player collider
     */
    private void OnTriggerExit(Collider other) {
        Debug.Log("Something exit moving platform");
        if (other.CompareTag("Player")) {
            _player.transform.parent = null;
        }
    }
}
