using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
	protected TriggerState currentState;

	protected virtual void Awake() {
		currentState = new TriggerState(false, false);
	}

	protected virtual void Update() {
		if (currentState.just) currentState.just = false;
	}

	public void SetState(bool active, bool forceSpike = false) {
		if (forceSpike || active != currentState.active) {
			currentState.active = active;
			currentState.just = true;
		}
	}

	public TriggerState GetTriggerState() {
		return currentState;
	}


}

public class TriggerState {
	public bool just;
	public bool active;

	public TriggerState(bool active, bool just) {
		this.just = just;
		this.active = active;
	}
}
