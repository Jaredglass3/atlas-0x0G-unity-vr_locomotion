using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTransform : MonoBehaviour
{
    public Transform cameraTransform;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Rotate the Canvas to face the camera
        Vector3 direction = transform.position - cameraTransform.position;
        direction.x = direction.z = 0; // Keep the health bar upright, only rotate around Y axis
        transform.LookAt(cameraTransform.position - direction); 
    }
}