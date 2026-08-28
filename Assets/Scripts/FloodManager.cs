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
        transform.position += new Vector3(0, speed * waterstate, 0);
    }

    private void Rise()
    {
        while (transform.position.y < maxheight)
        {
            waterstate = 1;
        }
        waterstate = 0;
    }

    private void Lower()
    {
        while (transform.position.y > minheight)
        {
            waterstate = -1;
        }
        waterstate = 0;
    }
}
