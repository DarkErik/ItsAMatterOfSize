using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : Trigger
{
	[SerializeField] private float interactableRange = 0.5f;
	[SerializeField] private GameObject attentionSign;
	[SerializeField] private Transform attentionPosition;

	private GameObject attention;

	protected override void Awake() {
		base.Awake();
		if (attentionPosition == null) attentionPosition = transform;
	}

	protected override void Update() {
		base.Update();
		if (PlayerControler.instance.enabled && Util.PointInCircle(PlayerControler.instance.transform.position, transform.position, interactableRange)) {
			if (attention == null && attentionSign != null) attention = Instantiate(attentionSign, attentionPosition);
			
			if (Input.GetButtonDown("Fire1")) {
				SetState(true, true);
			}
		} else {
			if (attention != null) Destroy(attention);
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, interactableRange);
	}

}
