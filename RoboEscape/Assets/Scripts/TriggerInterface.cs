using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerInterface : MonoBehaviour {
    [SerializeField] protected bool isActivated;
    public bool getIsActivated => isActivated;

    protected CableController CableController;
    protected SoundManager SoundManager;
}
