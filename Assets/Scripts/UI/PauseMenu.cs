using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button resumebutton;
    public Button quitbutton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        resumebutton.onClick.AddListener(ResumeButtonCallBack);
        quitbutton.onClick.AddListener(QuitButtonCallBack);
    }

    void OnDisable()
    {
        resumebutton.onClick.RemoveListener(ResumeButtonCallBack);
        quitbutton.onClick.RemoveListener(QuitButtonCallBack);
    }

    private void ResumeButtonCallBack()
    {
        print("i'm just a lawnmower");
        gameObject.SetActive(false);
    }

    private void QuitButtonCallBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
