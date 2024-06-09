using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private string LinkedInAccount = "https://www.linkedin.com/in/abdelrhmanuzaki/";

    public AudioSource BirdBackGroundSound;

    private void Start()
    {
        if (BirdBackGroundSound != null)
        {
            BirdBackGroundSound.Play();
        }
        else
        {
            Debug.LogWarning("BirdBackGroundSound is not set");
        }

    }
    public void HowToPlayButton()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("ChooseLevelScene"); 
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void ContactUs()
    {
        Application.OpenURL(LinkedInAccount);
    }
}
