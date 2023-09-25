using UnityEngine;
/*
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
*/

public class shootLaser : MonoBehaviour
{
    public Material material;
    private GameObject firstLaser;

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find(this.name + "LaserBeam"));

        firstLaser = new GameObject(this.name + "LaserBeam");
        Laser laserComponent = firstLaser.AddComponent<Laser>();
        laserComponent.InitLaser(transform.position, transform.right, material, false);
        
        
        
    }
}

