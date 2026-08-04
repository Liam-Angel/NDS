using UnityEngine;
using System.Collections;

public class LightningSpawner : MonoBehaviour
{
    [SerializeField] GameObject kaboom;
    Vector3 pos;
    public float interval;

    public float height;
    public float range;

    public float xmin;
    public float xmax;
    public float zmin;
    public float zmax;

    private bool wait;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wait == false)
        {
            StartCoroutine(DelayAction(interval));
            wait = true;
        }
    }

    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        pos = new Vector3(Random.Range(xmin, xmax), height, Random.Range(zmin, zmax));
        Collider[] hits = Physics.OverlapBox(pos, new Vector3(range, 80, range));
        Vector3 peak = new Vector3(0, 0, 0);
        float maxy = Mathf.NegativeInfinity;

        foreach (Collider col in hits)
        {
            float topy = col.bounds.max.y;
            if (topy > maxy)
            {
                maxy = topy;
                peak = new Vector3(col.bounds.center.x, topy, col.bounds.center.z);
            }
        }

        Instantiate(kaboom, peak, transform.rotation);
        wait = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        Gizmos.DrawCube(pos, new Vector3(range, 80, range));
    }
}
