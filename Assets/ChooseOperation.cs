using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseOperation : MonoBehaviour
{
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

    public void SetOperation(string operation)
    {
        PlayerPrefs.SetString("Operation", operation);
        SceneManager.LoadScene("PlayingScene");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void GoBack()
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
