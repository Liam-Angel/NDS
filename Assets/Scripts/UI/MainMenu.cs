using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playbutton;
    public Button quitbutton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        playbutton.onClick.AddListener(PlayButtonCallBack);
        quitbutton.onClick.AddListener(() => QuitButtonCallBack());
    }

    void OnDisable()
    {
        playbutton.onClick.RemoveListener(PlayButtonCallBack);
        quitbutton.onClick.RemoveListener(QuitButtonCallBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayButtonCallBack()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitButtonCallBack()
    {
        Application.Quit();            
    }
}
