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
	[SerializeField] private bool fireOnlyWhenInDriectLine = true;
	[SerializeField] private bool fireOnlyWhenLookingInRightDir = true;
	

	[SerializeField] private float beforeShootDelay = 0f;
	[SerializeField] private GameObject onTargetLockedEffect = null;
	[SerializeField] private Transform effectPostion = null;



	private bool isShooting = false;
	private BasicMovement moveScript;
	private float shootTimeStamp = 0f;

	private void Awake() {
		moveScript = GetComponent<BasicMovement>();
		if (effectPostion == null) effectPostion = gameObject.transform;
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
					if ((!fireOnlyWhenInDriectLine || !Physics2D.Linecast(weapon.GetShootingOrigin().position, PlayerControler.instance.transform.position, Factory.instance.groundMask)) 
						&& (!fireOnlyWhenLookingInRightDir || moveScript.MovingRight() == transform.position.x - PlayerControler.instance.transform.position.x < 0)) {
						moveScript.Shutdown();
						isShooting = true;
						shootTimeStamp = Time.time + beforeShootDelay;

						if (onTargetLockedEffect != null) {
							GameObject go = Instantiate(onTargetLockedEffect, effectPostion);
							go.transform.localScale *= 1 / effectPostion.lossyScale.x;
						}

					} else {
						return;
					}
				}

				if (Time.time >= shootTimeStamp) {
					Vector2 direction = PlayerControler.instance.transform.position - transform.position;

					weapon.Fire(((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + Random.Range(-aimSpread, aimSpread)) % 360);
				}
			}
		} else {
			if (!moveScript.enabled) moveScript.enabled = true;
		}
	}

	public void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, activeRange);
	}

}
