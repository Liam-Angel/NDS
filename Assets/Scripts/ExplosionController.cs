using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
    public float radius;
    public float power;
    public float upwardsmodifier;
    public ForceMode forcemode = ForceMode.Force;
    public LayerMask layers;
    public float volume;
    public AudioClip[] sound;

    private Light flash;
    public float bright;
    void Start()
    {
        flash = GetComponent<Light>();
        int rsound = Random.Range(0, 3);
        ApplyExplosionForce();
        AudioSource.PlayClipAtPoint(sound[rsound], transform.position, volume);
        StartCoroutine(DelayAction(1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyExplosionForce()
    {
        Vector3 pos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(pos, radius, layers);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddExplosionForce(power, pos, radius, upwardsmodifier, forcemode);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator DelayAction(float delayTime)
    {
        flash.intensity = bright;
        yield return new WaitForSeconds(0.05f);
        flash.intensity = 0f;
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
