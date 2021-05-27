using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private BasicWeapon basicWeapon;

    // Start is called before the first frame update
    private void Start()
    {
        basicWeapon = GetComponent<BasicWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (basicWeapon != null && Input.GetButton("Fire1"))
        {
            basicWeapon.Fire(getFireDirection());
        }
        else if (basicWeapon != null && Input.GetButton("Reload")) 
        {
            if (!basicWeapon.IsReloading() && basicWeapon.getMagazineSize() != basicWeapon.getCurrentAmmunition()) 
            {
                basicWeapon.ForceReload();
            } 
        }
        else {
            //Falls ein Fehler bei Awake passieren sollte
            basicWeapon = GetComponent<BasicWeapon>();
        }
    }

    private Vector2 getFireDirection() {
        Vector3 mousePos = Input.mousePosition;

        Vector2 direcVector = new Vector2(mousePos.x - Camera.main.scaledPixelWidth / 2, mousePos.y - Camera.main.scaledPixelHeight / 2);

        return direcVector;
    }
}
