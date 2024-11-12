using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGameScript : MonoBehaviour
{

    public void OnStartGame()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void OnExitGame()
    {
        Application.Quit();
    }

}