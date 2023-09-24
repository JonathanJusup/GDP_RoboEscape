using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    private Vector3 pos, dir;
    private GameObject laserObject;
    private LineRenderer laser;
    private List<Vector3> laserVertices = new List<Vector3>();

    private GameObject newSource;

    public LaserBeam(Vector3 pos, Vector3 dir, Material mat)
    {
        this.pos = pos;
        this.dir = dir;

        this.laserObject = new GameObject();
        this.laserObject.name = "Laser Beam";
     
        this.laser = new LineRenderer();
        this.laser = laserObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = mat;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;

        castRay(pos, dir, laser);
    }

    void castRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserVertices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserVertices.Add(ray.GetPoint(30));
        }
        UpdateLaser();
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserVertices.Count;
        
        foreach (Vector3 vertex in laserVertices)
        {
            laser.SetPosition(count++, vertex);
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if (hitInfo.collider.gameObject.CompareTag("Mirror"))
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            
            castRay(pos, dir, laser);
        }
        else if (hitInfo.collider.gameObject.CompareTag("Transparent"))
        {
            Debug.Log("HIT TRANSPARENT");

            newSource = new GameObject("Second");
            newSource.transform.position = hitInfo.point;
            newSource.transform.right = direction;
            newSource.AddComponent<shootLaser>();
            
            Object.Destroy(newSource);

        }
        else
        {
            laserVertices.Add(hitInfo.point);
            UpdateLaser();
        }
    }
}

