using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsSceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject regAIrock;
    [SerializeField] private GameObject paintRegirock;

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

    private void Awake()
    {
        ChooseRegirock();
    }

    private void ChooseRegirock() {
        int rand = Random.Range(0, 4);

        Debug.Log("Rolled a " + rand + " while selecting Regirock");

        if (rand == 0)
        {
            regAIrock.SetActive(false);
            paintRegirock.SetActive(true);
        }
        else 
        {
            regAIrock.SetActive(true);
            paintRegirock.SetActive(false);
        }
    }
}
