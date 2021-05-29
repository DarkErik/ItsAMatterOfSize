using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision) {
		Entity ent = collision.gameObject.GetComponent<Entity>();
		if (ent != null) {
			if(ent.gameObject == PlayerControler.instance.gameObject) {
				Time.timeScale = 0f;
				StartCoroutine(RespawnAfterSecond());
			} else {
				ent.Kill();
			}
		}


	}


	private IEnumerator RespawnAfterSecond() {
		yield return new WaitForSecondsRealtime(1f);
		Entity ent = PlayerControler.instance.gameObject.GetComponent<Entity>();
		if(ent.gameObject == PlayerControler.instance.gameObject) {
			
			if (!ent.Damage(1, default)) {
				RespawnPoint.Respawn();
			}
		}
		Time.timeScale = 1f;
		
	}
}
