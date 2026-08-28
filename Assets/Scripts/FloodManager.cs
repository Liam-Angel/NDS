using UnityEngine;

public class FloodManager : MonoBehaviour
{
    public float minheight;
    public float maxheight;
    public float speed;

    private int waterstate = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        DisasterManager.DisasterStart += Rise;
        DisasterManager.DisasterStop += Lower;
    }

    private void OnDisable()
    {
        DisasterManager.DisasterStart -= Rise;
        DisasterManager.DisasterStop -= Lower;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waterstate == 1 && transform.position.y > maxheight)
        {
            waterstate = 0;
        }
        if (waterstate == -1 && transform.position.y < minheight)
        {
            waterstate = 0;
        }

        transform.position += new Vector3(0, speed * waterstate, 0);
    }

    private void Rise()
    {
        waterstate = 1;
    }

    private void Lower()
    {
        waterstate = -1;
    }
}
