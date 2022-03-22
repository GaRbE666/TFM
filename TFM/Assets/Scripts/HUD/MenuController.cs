using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void GoToStartNewGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToOptionsScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToExitGame()
    {
        Application.Quit();
    }
}
