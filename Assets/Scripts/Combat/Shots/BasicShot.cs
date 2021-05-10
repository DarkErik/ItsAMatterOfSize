using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a very basic Shot.
/// If u create a new one, dont forget to call Init() !
/// </summary>
public class BasicShot : MonoBehaviour
{
	[SerializeField] protected int damage = 2;
	[SerializeField] protected float speed = 10;
	[SerializeField] protected float knockbackStrenght = 2f;
	protected bool isEnemy;

	/// <summary>
	/// Initializes the shot
	/// </summary>
	/// <param name="isEnemy">whether the shot is in the enemy team</param>
	public virtual void Init(bool isEnemy) {
		this.isEnemy = isEnemy;

		if (isEnemy) {
			this.gameObject.layer = LayerMask.NameToLayer("EnemyBullets");
		} else {
			this.gameObject.layer = LayerMask.NameToLayer("PlayerBullets");
		}
	}

	public void Update() {
		Move();
	}

	/// <summary>
	/// Movement of the Shot (overrideable!)
	/// </summary>
	public virtual void Move() {
		transform.position += transform.right * speed * Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		Entity collEntity = collision.gameObject.GetComponent<Entity>();
		if (collEntity != null) {
			OnHitEntity(collEntity);
		} else {
			OnHitEnviroment(collision.gameObject);
		}
	}

	/// <summary>
	/// Handles the hit with an entity.
	/// Override this if needed!
	/// </summary>
	/// <param name="hit">The hit Entity</param>
	protected virtual void OnHitEntity(Entity hit) {
		hit.TryDamage(damage, hit.GetKnockbackOutOfPosition(this.gameObject, knockbackStrenght));
		Kill();
	}

	/// <summary>
	/// Handles the hit with any part of the Enviroment (e.x. the ground) (NO ENTITIES!)
	/// </summary>
	/// <param name="hit">The hit gameobject</param>
	protected virtual void OnHitEnviroment(GameObject hit) {
		Kill();
	}

	/// <summary>
	/// Destroys this shot!
	/// </summary>
	public virtual void Kill() {
		Destroy(this.gameObject);
	}
}
