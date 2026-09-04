using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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

    private bool game;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        DisasterManager.DisasterStart += Go;
        DisasterManager.DisasterStop += Stop;
        StartCoroutine(DelayAction(interval));
    }

    // Update is called once per frame
    void OnDisable()
    {
        DisasterManager.DisasterStart -= Go;
        DisasterManager.DisasterStop -= Stop;
    }

    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(5);
        while(true) // loops forever
        {
            yield return new WaitForSeconds(delayTime); // waits a set amount of time
            Collider hitcol = null;
            pos = new Vector3(Random.Range(xmin, xmax), height, Random.Range(zmin, zmax)); // picks a random spot on the map
            Collider[] hits = Physics.OverlapBox(pos, new Vector3(range, boxheight, range), transform.rotation, layers); // gets the collider of every object in th area
            Vector3 peak = new Vector3(0, 0, 0);
            float maxy = -90;

            foreach (Collider col in hits) // iterates through each collider
            {

                float topy = col.bounds.max.y; // gets the highest point of the collider
                if (topy > maxy) // if the current value is larger than the previous largest
                {
                    maxy = topy; // saves as largest
                    hitcol = col;
                    peak = new Vector3(col.bounds.center.x, maxy, col.bounds.center.z); // position of the highest point at the center of the object
                }
            }

            if (peak == new Vector3(0, 0, 0)) // strikes ground level if no objects are found
            {
                peak = new Vector3(pos.x, floor, pos.z);
            }

            Instantiate(kaboom, peak, transform.rotation); // creates an explosion          
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

    void Go()
    {
        game = true;
    }

    void Stop()
    {
        game = false;
    }
}
