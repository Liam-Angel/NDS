using System;
using System.Collections;
using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    public float grace;
    public float gametime;
    public static event Action DisasterStart;
    public static event Action DisasterStop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Coroutine gameroutine;
    void OnEnable()
    {
        gameroutine = StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void OnDisable()
    {
        if(gameroutine != null)
        {
            StopCoroutine(gameroutine);
        }
    }

    IEnumerator CountDown()
    {
        print("start");
        yield return new WaitForSeconds(grace);
        print("ready");
        DisasterStart?.Invoke();
        print("done");
        yield return new WaitForSeconds(gametime);
        DisasterStop?.Invoke();
        print("again");
    }
}
