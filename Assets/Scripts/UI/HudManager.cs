using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class HudManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float grace;
    public float gametime;
    public float rest;
    public RectTransform bar;
    public float height;
    public float width;
    [SerializeField] private PlayerDamage playerdamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GameTimer());
    }

    // Update is called once per frame
    void Update()
    {
        bar.sizeDelta = new Vector2(math.remap(0, 100, 0, width, playerdamage.health), height);
    }
    IEnumerator GameTimer()
    {
        float timer = grace;
        while(timer > 0)
        {
            text.text = ("Game Starts In " + timer);
            yield return new WaitForSeconds(1);
            timer--;
            
        }
        timer = gametime;
        while (timer > 0)
        {
            text.text = ("Game Ends In " + timer);
            yield return new WaitForSeconds(1);
            timer--;
        }
        timer = rest;
        while (timer > 0)
        {
            text.text = ("Hooray");
            yield return new WaitForSeconds(1);
            timer--;
            
        }
        SceneManager.LoadScene("MainMenu");

    }

}
