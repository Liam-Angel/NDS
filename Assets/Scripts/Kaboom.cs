using UnityEngine;

public class Kaboom : MonoBehaviour
{
    public GameObject kaboom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(kaboom, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
