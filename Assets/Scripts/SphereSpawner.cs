using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] Transform tr;
    [SerializeField] GameObject bullet;
    public float delaytime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        StartCoroutine(DelayAction(delaytime));
    }

    private void OnDisable()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayAction(float delayTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(delayTime);
            Instantiate(bullet, (tr.position + 3 * transform.forward), tr.rotation);
            Debug.Log("bang");
        }
            
    }

}