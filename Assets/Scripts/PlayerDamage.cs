using System.Collections;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerDamage : MonoBehaviour
{
    public Transform water;
    public Transform cam;
    public Rigidbody rb;
    public PlayerMovement playermovement;
    public float health;
    public float maxair;
    public float damagescale;
    public float threshold;
    public float masseffect;
    public float difeffect;
    private bool drowning = false;
    private bool dead = false;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && dead == false)
        {
            StartCoroutine(Die());
            dead = true;
        }

        if (cam.position.y < water.position.y && drowning == false)
        {
            drowning = true;
            StartCoroutine(Drowning());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject thing = collision.gameObject;
        if (thing.TryGetComponent<Rigidbody>(out Rigidbody objectrb))
        {
            Vector3 pv = rb.linearVelocity.normalized; //normalize the player velocity
            Vector3 ov = objectrb.linearVelocity.normalized; //normalize the object velocity
            float dif = Vector3.Angle(pv, ov); //difference in velocity direction between object and player

            float applieddif = Mathf.Clamp(dif * difeffect, 0, 180); //adjusts the dif value to work with speed dif calculation
                                                                     //
            float speeddif = Mathf.Clamp((objectrb.linearVelocity.magnitude * dif) - rb.linearVelocity.magnitude, 0.1f, 99); //difference in speed relative to direction

            float damage = (speeddif * damagescale); // calculates the damage value
            print(dif);
            if (damage > threshold)
            {
                health -= damage;
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
        drowning = false;
    }

    IEnumerator Die()
    {
        rb.constraints = RigidbodyConstraints.None;
        playermovement.enabled = false;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}