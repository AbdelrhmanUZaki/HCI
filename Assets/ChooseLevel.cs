using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
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

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnDifficultyButtonClick(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene("ChooseOperatorScene");
    }
}
