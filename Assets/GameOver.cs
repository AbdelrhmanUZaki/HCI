using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public AudioSource GameOverSound;

    private void Start()
    {
        GameOverSound.Play();  

    }
    public void OnPlayAgain()
    {
        SceneManager.LoadScene("ChooseLevelScene"); 
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
