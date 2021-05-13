using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : BasicMovement
{
	private static readonly float NOT_MOVED_THRESHHOLD_SQR = 0.001f * 0.001f;

	[SerializeField] private float speed = 4f;
	[SerializeField] private bool lookingRight = true;

	[SerializeField] private Transform rotationBase;
	[SerializeField] private Transform groundCheck;

	[SerializeField] private float groundCheckRadius = 0.2f;

	private Vector3 lastPosition = Vector3.zero;
	private bool flipedMidAir = false;




	public void FixedUpdate() {

		if (groundCheck != null && !Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Factory.instance.groundMask)) {
			if (!flipedMidAir) {
				flipedMidAir = true;
				Flip();
			}
		} else {
			flipedMidAir = false;
		}

		if (!flipedMidAir && (transform.position - lastPosition).sqrMagnitude < NOT_MOVED_THRESHHOLD_SQR * Time.fixedTime) {
			Flip();
			flipedMidAir = true;
		}

		rb.velocity = new Vector2(lookingRight ? speed : -speed, rb.velocity.y);
		lastPosition = transform.position;

	}

	public void Flip() {
		lookingRight = !lookingRight;
		rotationBase.rotation = Quaternion.Euler(rotationBase.rotation.eulerAngles.x, (rotationBase.rotation.eulerAngles.y + 180) % 360, rotationBase.rotation.eulerAngles.z);
	}

	public override bool MovingRight() {
		return lookingRight;
	}

	private void OnDrawGizmosSelected() {
		if (groundCheck != null) Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
	}
}
