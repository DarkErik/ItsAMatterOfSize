using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyWeaponAttack : MonoBehaviour
{
	[SerializeField] private BasicWeapon weapon;
	[Range(0, 90)][SerializeField]
	private float aimSpread = 5f;

	[SerializeField] private float activeRange = 50f;

	/// <summary>
	/// Only fires, when a line of view is established
	/// </summary>
	[SerializeField] private bool fireSmart = true;

	private bool isShooting = false;
	private BasicMovement moveScript;

	private void Awake() {
		moveScript = GetComponent<BasicMovement>();
	}

	public void Update() {
		
		if (Util.PointInCircle(PlayerControler.instance.transform.position, transform.position, activeRange)) {

			if (weapon.IsReloading()) {
				if (isShooting) {
					moveScript.WakeUp();
					isShooting = false;
					
				}
			} else {
				if (!isShooting) {
					if (!fireSmart || !Physics2D.Linecast(weapon.GetShootingOrigin().position, PlayerControler.instance.transform.position, Factory.instance.groundMask)) {
						moveScript.Shutdown();
						isShooting = true;
					} else {
						weapon.ForceReload();
						return;
					}
				}
				Vector2 direction = PlayerControler.instance.transform.position - transform.position;

				weapon.Fire(((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + Random.Range(-aimSpread, aimSpread)) % 360);
			}
		} else {
			if (!moveScript.enabled) moveScript.enabled = true;
		}
	}

	public void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, activeRange);
	}

}
