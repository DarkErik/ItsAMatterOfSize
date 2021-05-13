using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Baseclass for all Enemy Movement behaivours
/// </summary>
public abstract class BasicMovement : MonoBehaviour
{
	protected Rigidbody2D rb;
	protected virtual void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void Shutdown() {
		this.enabled = false;
		rb.velocity = new Vector2(0, rb.velocity.y);
	}

	public void Shutdown(float time) {
		Shutdown();
		StartCoroutine(_WakeUpAfterSeconds(time));
	}

	private IEnumerator _WakeUpAfterSeconds(float time) {
		yield return new WaitForSeconds(time);
		WakeUp();
	}

	public void WakeUp() {
		this.enabled = true;
	}
}
