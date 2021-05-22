using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
