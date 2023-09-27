using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool isDeadly;
    private Vector3 pos, dir;
    private Material mat;
    private LineRenderer lineRenderer;

    private Color greenColor = new Color(0.0f, 1.0f, 0.0f, 0.8f);
    private Color redColor = new Color(1.0f, 0.0f, 0.0f, 0.8f);

    public void InitLaser(Vector3 position, Vector3 direction, Material material, bool isDeadly)
    {
        this.pos = position;
        this.dir = direction;
        this.mat = material;
        this.isDeadly = isDeadly;

        InitLineRenderer();
    }

    private void InitLineRenderer()
    {
        lineRenderer = this.AddComponent<LineRenderer>();
        lineRenderer.material = mat;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, pos);

        if (isDeadly)
        {
            lineRenderer.material.color = redColor;
            lineRenderer.startColor = redColor;
            lineRenderer.endColor = redColor;
        }
        else
        {
            lineRenderer.material.color = greenColor;
            lineRenderer.startColor = greenColor;
            lineRenderer.endColor = greenColor;
        }

        

        
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100, 1))
        {
            ProcessHit(hit);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.GetPoint(100));
        }
    }

    private void ProcessHit(RaycastHit hit)
    {
        lineRenderer.SetPosition(1, hit.point);
        
        //Create Child Laser depending on what is hit        
        if (hit.collider.gameObject.CompareTag("Mirror"))
        {
            LaserReflection(hit);
        }
        else if (hit.collider.gameObject.CompareTag("Transparent_Red"))
        {
            LaserTransmission(hit, true);
        }
        else if (hit.collider.gameObject.CompareTag("Transparent_Green"))
        {
            LaserTransmission(hit, false);
        }
        else if (hit.collider.gameObject.CompareTag("Transparent"))
        {
            LaserTransmission(hit, isDeadly);
        }
        else if (hit.collider.gameObject.CompareTag("Player") && this.isDeadly)
        {
            
            PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.Die();
            
        }
        else if (hit.collider.gameObject.CompareTag("Meltable") && this.isDeadly)
        {
            MeltingController meltingController = GameObject.Find("MeltingBlock").GetComponent<MeltingController>();
            meltingController.StartMelting();
        }
    }

    private void LaserReflection(RaycastHit hit)
    {
        GameObject reflection = new GameObject("Reflection");
        reflection.transform.parent = transform;
        reflection.transform.position = hit.point;
        reflection.transform.right = Vector3.Reflect(dir, hit.normal);

        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, mat, isDeadly);
    }

    private void LaserTransmission(RaycastHit hit, bool isDeadly)
    {
        GameObject reflection = new GameObject("Transmission");
        reflection.transform.parent = transform;
        reflection.transform.position = hit.point + dir * 0.01f;
        reflection.transform.right = dir;

        Laser reflectionLaserComponent = reflection.AddComponent<Laser>();
        reflectionLaserComponent.InitLaser(reflection.transform.position, reflection.transform.right, mat, isDeadly);
    }
}
