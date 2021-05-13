using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Everything what can take damage is an Entity
/// </summary>
public class Entity : MonoBehaviour
{
	protected int hp;
	[SerializeField] protected int maxHp = 10;
	[SerializeField] protected bool isEnemy = true;
	[SerializeField] protected int touchDamage = 1;
	[SerializeField] protected float touchKnockback = 3;
	[SerializeField] protected float invincibilityTimeAfterHit = 0f;

	[Range(0, 1)]
	[SerializeField] protected float knockbackResistance = 0.5f;

	private float currentInvincibilityTime = 0f;
	private Rigidbody2D rb;

	protected virtual void Awake() {
		hp = maxHp;
		rb = GetComponent<Rigidbody2D>();

		if (isEnemy) {
			this.gameObject.layer = LayerMask.NameToLayer("Enemy");
		} else {
			this.gameObject.layer = LayerMask.NameToLayer("Player");
		}
	}

	protected virtual void Update() {
		if (currentInvincibilityTime > 0) currentInvincibilityTime -= Time.deltaTime;
	}

	/// <summary>
	/// Trys to Damage an enemy
	/// </summary>
	/// <param name="damage">the taken damage</param>
	/// <param name="knockback">Use the Entity.GetKnockback functions for this param!</param>
	public void TryDamage(int damage, Vector3 knockback) {
		if (!IsInvincible()) Damage(damage, knockback);
	}


	/// <summary>
	/// Damages a Unit. USUALLY you NOT call this method directly. Use TryDamage instead!
	/// Only call this method, when you not care about an entity possible beeing invincible at the moment!
	/// </summary>
	/// <param name="damage"></param>
	/// <param name="knockback"></param>
	public void Damage(int damage, Vector3 knockback) {
		hp -= damage;
		if (hp <= 0) {
			Kill();
		} else {
			if (knockbackResistance < 1) {
				if (IsPlayer()) PlayerControler.Shutdown(0.2f);

				rb.velocity = knockback * (1 - knockbackResistance);
			}
		}
	}

	/// <summary>
	/// Returns whether an Entity invincible at the moment
	/// </summary>
	/// <returns></returns>
	public bool IsInvincible() {
		return currentInvincibilityTime > 0;
	}

	/// <summary>
	/// Returns whether this entity is the Player
	/// </summary>
	/// <returns></returns>
	public bool IsPlayer() {
		return PlayerControler.instance.gameObject == this.gameObject;
	}

	/// <summary>
	/// Kills the Entity!
	/// USE THIS METHOD, NOT DESTROY! U FOOL
	/// </summary>
	public virtual void Kill() {
		Destroy(this.gameObject);
	}
	
	/// <summary>
	/// Calculates the Knockback positionbased. USE THIS FOR (TRY)DAMAGE Functions
	/// </summary>
	/// <param name="attacker">the attacking gameObj</param>
	/// <param name="knockbackStrenght">The power of the knockback</param>
	/// <returns></returns>
	public Vector3 GetKnockbackOutOfPosition(GameObject attacker, float knockbackStrenght) {
		Vector3 dir = (this.transform.position - attacker.transform.position).normalized * knockbackStrenght;
		if (dir.y < 13) dir.y += 13;
		return dir ;
	}

	public bool IsEnemy() {
		return isEnemy;
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		if (touchDamage <= 0) return;
		Entity ent = collision.gameObject.GetComponent<Entity>();
		if (ent != null && ent.IsEnemy() != this.IsEnemy()) {
			ent.TryDamage(touchDamage, ent.GetKnockbackOutOfPosition(gameObject, touchKnockback));
		}
	}

}

