using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneTransition : MonoBehaviour
{
    public void startTransition()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void Update()
    {
        if (Input.GetButton("Fire1")) 
        {
            startTransition();
        }
    }
}
