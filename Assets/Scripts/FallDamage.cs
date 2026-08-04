using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public Rigidbody rb;
    public float thresh;
    public float health = 100f;
    public float falldamage;
    Vector3 pv;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diffv = (rb.linearVelocity - pv);
        float diff = ((diffv.x + diffv.z)/2 + diffv.y);

        if(diff > thresh)
        {
            health -= (diff * falldamage);
            //print(health);
            print(diff);
        }
        
        pv = rb.linearVelocity;
    }
}
