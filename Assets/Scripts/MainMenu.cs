using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Multiplayer()
    {
        SceneManager.LoadScene("Map");
    }

    public void Showcase()
    {
        SceneManager.LoadScene("Showcase");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
