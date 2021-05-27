using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAmmo : MonoBehaviour
{
    private int maxAmmo = 25;
    private int currentAmmo;  

    private BasicWeapon playerWeapon;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = PlayerControler.instance.GetComponent<BasicWeapon>();

        maxAmmo = playerWeapon.getMagazineSize();
        currentAmmo = maxAmmo;

        text.SetText(currentAmmo + "/" + maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        //Check ob es den Controller noch gibt
        if (playerWeapon == null)
        { 
            playerWeapon = PlayerControler.instance.GetComponent<BasicWeapon>();
        }

        //Update des Textes, wenn Aenderung
        if (playerWeapon.getCurrentAmmunition() != currentAmmo)
        {
            currentAmmo = playerWeapon.getCurrentAmmunition();

            text.SetText(currentAmmo + "/" + maxAmmo);
        }
    }
}
