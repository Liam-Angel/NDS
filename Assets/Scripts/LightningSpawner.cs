using UnityEngine;
using System.Collections;

public class LightningSpawner : MonoBehaviour
{
    [SerializeField] GameObject kaboom;
    [SerializeField] LayerMask layers;
    Vector3 pos;

    public float interval;

    public float height;
    public float boxheight;
    public float range;

    public float floor;

    public float xmin;
    public float xmax;
    public float zmin;
    public float zmax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayAction(interval));
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    IEnumerator DelayAction(float delayTime)
    {
        while(true)
        {
            Collider hitcol = null;
            yield return new WaitForSeconds(delayTime);
            
            pos = new Vector3(Random.Range(xmin, xmax), height, Random.Range(zmin, zmax));
            Collider[] hits = Physics.OverlapBox(pos, new Vector3(range, boxheight, range), transform.rotation, layers);
            Vector3 peak = new Vector3(0, 0, 0);
            float maxy = -90;

            

            foreach (Collider col in hits)
            {

                float topy = col.bounds.max.y;
                if (topy > maxy)
                {
                    maxy = topy;   
                    hitcol = col;
                    peak = new Vector3(col.bounds.center.x, maxy, col.bounds.center.z);
                }
            }
            //GameObject hitobject = hitcol.gameObject;

            if (peak == new Vector3(0, 0, 0))
            {
                peak = new Vector3(pos.x, floor, pos.z);
            }


            print(peak);
            Instantiate(kaboom, peak, transform.rotation);

        }
    }
        

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        Gizmos.DrawCube(pos, new Vector3(range * 2, boxheight * 2, range * 2));
    }

    void DetachChunk(GameObject chunk)
    {
       
    }
}
