using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Performer : MonoBehaviour
{
	[SerializeField] protected Trigger input;
	[SerializeField] protected bool onlyPerformOnce = false;

	protected bool stopPerforming = false;

	public void Update() {
		if (stopPerforming) return;

		TriggerState state = input.GetTriggerState();
		if (onlyPerformOnce && state.active) stopPerforming = true;
		Perform(state);
	}

	protected abstract void Perform(TriggerState state);


}
