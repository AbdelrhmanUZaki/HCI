using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{

    public void GoHome()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }

}
