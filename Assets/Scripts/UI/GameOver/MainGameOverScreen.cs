using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameOverScreen : MonoBehaviour
{
    [SerializeField] private SceneObj mainMenu;

    public void LoadMainMenu()
    {
        SceneTransition.LoadScene(mainMenu.name);
    }

    public void Respawn()
    {
		UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("GameOver");
		Time.timeScale = 1f;
        RespawnPoint.Respawn();
    }
}
