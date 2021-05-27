using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
	[SerializeField] private GameObject shot;
	[SerializeField] private int magazineSize = 10;
	[SerializeField] private float reloadTime = 1f;
	[SerializeField] private float shotsPerSecound = 2;
	[SerializeField] private Transform shotOrigin;
	[SerializeField] private bool isEnemy = false;

	private int currentAmmunition;
	private float nextTimeShot = 0;
	private float reloadTimestamp = -1;


	public void Awake() {
		currentAmmunition = magazineSize;
		Entity entity = GetComponent<Entity>();
		if (entity != null) {
			isEnemy = entity.IsEnemy();
		}
	}

	public void Fire(float angleDEG) {
		if (canShoot()) {
			SoundManager.PlaySound(Sound.SHOT, true);
			createBullet(angleDEG);
			nextTimeShot = Time.time + 1 / shotsPerSecound;
			currentAmmunition--;
			if (currentAmmunition <= 0) {
				if (gameObject == PlayerControler.instance.gameObject) SoundManager.PlaySound(Sound.RELOAD, false);
				reloadTimestamp = Time.time + reloadTime;
			}
		}
	}

	public void Fire(Vector2 direction) {
		Fire(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
	}

	private bool canShoot() {
		return Time.time >= nextTimeShot && currentAmmunition >= 1 && reloadTimestamp == -1;
	}


	private void createBullet(float angleDEG) {
		var temp = Instantiate(shot);
		temp.transform.rotation = Quaternion.Euler(0,0,angleDEG);
		temp.transform.position = shotOrigin.position;
		temp.GetComponent<BasicShot>().Init(isEnemy);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float reloadPercentage() {
		if (reloadTimestamp < 0) {
			return 1;
		}
		return (Time.time - reloadTimestamp) / reloadTime; 
	}

	public int getCurrentAmmunition() {
		return currentAmmunition;
	}

	public int getMagazineSize() {
		return magazineSize;
	}

    public void Update()
    {
		if (reloadTimestamp >= 0 && Time.time >= reloadTimestamp) {
			reloadTimestamp = -1;
			currentAmmunition = magazineSize;
		}
    }

	public bool IsReloading() {
		return reloadTimestamp != -1;
	}

	public Transform GetShootingOrigin() {
		return shotOrigin;
	}

	public void ForceReload() {
		if (gameObject == PlayerControler.instance.gameObject) SoundManager.PlaySound(Sound.RELOAD, false);
		reloadTimestamp = Time.time + reloadTime;
	}
}
