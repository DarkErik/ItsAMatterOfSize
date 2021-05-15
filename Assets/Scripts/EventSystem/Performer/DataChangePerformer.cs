using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChangePerformer : Performer
{
	[SerializeField] private TriggerDataMode mode;
	[SerializeField] private string flag;
	[SerializeField] private bool value = true;
	[SerializeField] private bool invertBehaiviorOnTriggerReleased = false;


	protected override void Perform(TriggerState state) {
		if (state.just) {
			if (state.active) {
				switch (mode) {
					case TriggerDataMode.FLAG_EXIST:
						TriggerData.EnsureFlag(flag);
						break;
					case TriggerDataMode.BOOLEAN_VALUE:
						TriggerData.SetBoolFlagValue(flag, value);
						break;
				}
			} else {
				if (!invertBehaiviorOnTriggerReleased) return;

				switch (mode) {
					case TriggerDataMode.FLAG_EXIST:
						TriggerData.flags.Remove(flag);
						break;
					case TriggerDataMode.BOOLEAN_VALUE:
						TriggerData.SetBoolFlagValue(flag, !value);
						break;
				}
			}
		}
	}

}
