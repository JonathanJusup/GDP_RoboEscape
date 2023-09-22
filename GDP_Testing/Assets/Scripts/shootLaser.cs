using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootLaser : MonoBehaviour
{
    public Material material;
    private LaserBeam beam;

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Laser Beam"));
        var o = gameObject;
        beam = new LaserBeam(o.transform.position, o.transform.right, material);
    }
}
