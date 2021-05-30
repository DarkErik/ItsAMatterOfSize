using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTimePerformer : Performer {

	[SerializeField] private float timeScale = 0;

	protected override void Perform(TriggerState state) {
		if (state.active && state.just) Time.timeScale = timeScale;
	}
}
