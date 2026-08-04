using UnityEngine;

public class Slide : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector3(speed, 0, 0);
    }
}
