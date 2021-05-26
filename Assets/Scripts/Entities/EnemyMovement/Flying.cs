using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : BasicMovement {
	[SerializeField] private float speed;
	[SerializeField] private float minDistanceFromPlayer = 4;
	[SerializeField] private Transform rotationBase;

	private bool flyingRight = false;
	public override bool MovingRight() {
		return flyingRight;
	}


	private void FixedUpdate() {
		Vector3 toPlayer = PlayerControler.instance.transform.position - transform.position;
		if (toPlayer.sqrMagnitude > minDistanceFromPlayer * minDistanceFromPlayer) {
			toPlayer.Normalize();
			if (toPlayer.x > 0 != flyingRight)
				Flip();

			transform.position += toPlayer * speed * Time.fixedDeltaTime;
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, minDistanceFromPlayer);
	}

	public void Flip() {
		flyingRight = !flyingRight;
		rotationBase.rotation = Quaternion.Euler(rotationBase.rotation.eulerAngles.x, (rotationBase.rotation.eulerAngles.y + 180) % 360, rotationBase.rotation.eulerAngles.z);
	}
}
