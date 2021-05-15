using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPerformer : Performer
{
	[SerializeField] private Animator anim;
	[SerializeField] private string justTriggeredTriggerName;
	[SerializeField] private string triggeredTriggerName;
	[SerializeField] private string justReleasedTriggerName;
	[SerializeField] private string releasedTriggerName;

	protected override void Perform(TriggerState state) {
		if (state.active) {
			if (state.just) {
				if (justTriggeredTriggerName == "") return;
				anim.SetTrigger(justTriggeredTriggerName);
			} else {
				if (triggeredTriggerName == "") return;
				anim.SetTrigger(triggeredTriggerName);
			}
		} else {
			if (state.just) {
				if (justReleasedTriggerName == "") return;
				anim.SetTrigger(justReleasedTriggerName);
			} else {
				if (releasedTriggerName == "") return;
				anim.SetTrigger(releasedTriggerName);
			}
		}
	}

}
