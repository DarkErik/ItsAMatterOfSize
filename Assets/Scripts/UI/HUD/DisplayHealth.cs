using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    private int maxHealth = 25;
    private int currentHealth;

    private Entity player;
    public TMP_Text text;

    void Start()
    {
        player = PlayerControler.instance.GetComponent<Entity>();

        maxHealth = player.GetMaxHp();
        currentHealth = player.GetHp();

        text.SetText(currentHealth + "/" + maxHealth);
    }
    void Update()
    {
        //Check ob es den Controller noch gibt
        if (player == null)
        {
            player = PlayerControler.instance.GetComponent<Entity>();
        }

        //Update des Textes, wenn Aenderung
        if (player.GetHp() != currentHealth)
        {
            currentHealth = player.GetHp();

            text.SetText(currentHealth + "/" + maxHealth);
        }
    }
}
