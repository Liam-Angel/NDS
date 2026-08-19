
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    public float upforce;
    private float surface;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private struct SubmergedObject
    {
        public Rigidbody rb;
        public Collider col;
    }

    private Dictionary<GameObject, SubmergedObject> submergedobjects = new Dictionary<GameObject, SubmergedObject>();
    void Start()
    {
        surface = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        surface = transform.position.y;
        foreach (var thing in submergedobjects)
        {
            SubmergedObject submergedobject = thing.Value;
            if(thing.Key != null)
            {
                Bounds bounds = submergedobject.col.bounds;

                float miny = bounds.min.y;
                float maxy = bounds.max.y;
                float height = maxy - miny;
                float volume = bounds.size.x * bounds.size.y * bounds.size.z;

                float depth = Mathf.Clamp(surface - miny, 0f, height);
                float submerged = volume * (depth / height);

                submergedobject.rb.AddForce(Vector3.up * submerged * upforce);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody objectrb) && other.attachedRigidbody.gameObject == other.gameObject)
        {
            GameObject thing = other.gameObject;
            submergedobjects.Add(thing, new SubmergedObject {rb = objectrb, col = other});
        }
    }

    private void OnTriggerExit(Collider other)
    {
        submergedobjects.Remove(other.gameObject);
    }
}
