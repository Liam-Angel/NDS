using UnityEngine;

public class ObjectCullingSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<FixedJoint>()!=null) //checks if the object has a joint
        {
            FixedJoint[] joints = collision.gameObject.GetComponents<FixedJoint>(); //gets all joints
            foreach (FixedJoint joint in joints) //loops through to destroy each joint
            {
                Destroy(joint);
            }
        }
        Destroy(collision.gameObject);
    }
}
