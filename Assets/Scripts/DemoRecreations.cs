using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;

public class DemoReceation : MonoBehaviour
{
    public Transform water;
    public Transform cam;
    public Rigidbody rb;
    public PlayerMovement playermovement;
    public float health;
    public float maxair;
    public float damagescale;
    public float threshold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Die());
        }

        if (cam.position.y < water.position.y)
        {
            StartCoroutine(Drowning());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject thing = collision.gameObject;
        if (thing.TryGetComponent<Rigidbody>(out Rigidbody objectrb)) //gets rigidbody if available
        {
            Vector3 dif = rb.linearVelocity - objectrb.linearVelocity; //difference between player and object velocity
            float damage = (dif.magnitude * objectrb.mass * damagescale); //calculates damage dealt
            if (damage > threshold) //minimum damage to avoid small collisions chipping away at health
            {
                health -= damage; // applies damage
            }
        }
    }

    IEnumerator Drowning()
    {
        float air = maxair;
        while (cam.position.y < water.position.y)
        {
            yield return new WaitForSeconds(0.1f);
            if (air <= 0)
            {
                health--;
            }
            else
            {
                air--;
            }
        }
        air = maxair;
    }

    IEnumerator Die()
    {
        rb.constraints = RigidbodyConstraints.None;
        playermovement.enabled = false;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}