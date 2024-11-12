using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnMenu()
    {

        SceneManager.LoadScene("StartScene"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
