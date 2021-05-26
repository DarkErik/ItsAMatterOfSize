using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameOverScreen : MonoBehaviour
{
    [SerializeField] private SceneObj mainMenu;
    [SerializeField] private SceneObj respawn;

    public void LoadMainMenu()
    {
        SceneTransition.LoadScene(mainMenu.name);
    }

    public void Respawn()
    {
        SceneTransition.LoadScene(respawn.name);
    }
}
