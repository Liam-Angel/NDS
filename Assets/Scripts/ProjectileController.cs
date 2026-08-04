using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public float speed;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity += (speed * transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
