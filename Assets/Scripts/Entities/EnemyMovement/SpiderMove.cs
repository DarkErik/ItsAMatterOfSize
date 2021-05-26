using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : BasicMovement
{
	[SerializeField] private float jumpForce = 0f;
	[SerializeField] private float jumpDelay = 1f;
	[SerializeField] private Vector2 jumpDirRight;

	private Animator anim;
	private Rigidbody2D rb;
	private float nextJump = 0;
	private bool right;
	private bool grounded = false;

	public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			grounded = true;
	}

	public void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			grounded = false;
	}

	public override bool MovingRight() {
		return right;
	}

	protected override void Awake() {
		base.Awake();
		anim = GetComponent<Animator>();
		nextJump = Time.time + jumpDelay;
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		if (Time.time > nextJump && grounded) {
			right = (transform.position - PlayerControler.instance.transform.position).x < 0;
			rb.AddForce(new Vector2(jumpDirRight.x * (right ? 1 : -1), jumpDirRight.y) * jumpForce);
			nextJump = Time.time + jumpDelay;
		}

		anim.SetBool("Jump", !grounded);
	}
}
