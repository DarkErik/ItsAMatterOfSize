using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsSceneTransition : MonoBehaviour
{
    [SerializeField] private SpriteRenderer regAIrock;
    [SerializeField] private Sprite[] regirocks;

    public void startTransition()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void Update()
    {
        if (Input.GetButtonUp("Fire1")) 
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
            regAIrock.sprite = regirocks[0];
        }
        else 
        {
            regAIrock.sprite = regirocks[1];
        }
    }
}
