using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDashTrigger : Performer {
	protected override void Perform(TriggerState state) {
		if (state.active && state.just) PlayerControler.instance.dashUnlocked = true;
	}
}
