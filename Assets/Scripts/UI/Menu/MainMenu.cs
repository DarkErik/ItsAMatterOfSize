using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private SceneObj startScene;
	[SerializeField] private SceneObj credits;

    public void LoadGame ()
    {
		SceneTransition.LoadScene(startScene.name);
    }

    public void LoadCredits()
    {
		SceneTransition.LoadScene(credits.name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
