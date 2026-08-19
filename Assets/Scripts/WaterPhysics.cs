using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    public float upforce;
    private List<GameObject> objects = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject other in objects)
        {
            Rigidbody objectrb = other.GetComponent<Rigidbody>();
            Transform objecttr = other.transform;
            Bounds bounds = GetComponent<Collider>().bounds;

            float miny = bounds.min.y;
            float maxy = bounds.max.y;
            float height = maxy - miny;
            float volume = bounds.size.x * bounds.size.y * bounds.size.z;

            float depth = Mathf.Clamp(transform.position.y - miny, 0f, height);
            float submerged = volume * (depth / height);

            objectrb.AddForce(Vector3.up * submerged * upforce);
        }
        objects.Clear();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.gameObject == other.gameObject)
        {
            objects.Add(other.gameObject);
        }
    }
}
