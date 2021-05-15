using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerDataMode {
	FLAG_EXIST,
	BOOLEAN_VALUE
}

public class TriggerData : Trigger
{
	public static LinkedList<string> flags = new LinkedList<string>();
	public static Dictionary<string, bool> boolFlags = new Dictionary<string, bool>();

	[SerializeField] private TriggerDataMode mode;
	[SerializeField] private string flag;
	[SerializeField] private bool desiredState = true;


	protected override void Update() {
		base.Update();

		switch (mode) {
			case TriggerDataMode.FLAG_EXIST:
				SetState(flags.Contains(flag));
				break;
			case TriggerDataMode.BOOLEAN_VALUE:
				SetState(GetBoolFlagState(flag) == desiredState);
				break;
		}
	}


	public static void EnsureFlag(string flag) {
		if (!flags.Contains(flag)) flags.AddLast(flag);
	}

	public static void SetBoolFlagValue(string flag, bool value) {
		if (!boolFlags.ContainsKey(flag)) {
			boolFlags.Add(flag, value);
		} else {
			boolFlags[flag] = value;
		}
	}

	public static bool GetBoolFlagState(string flag) {
		try {
			return boolFlags[flag];
		} catch {
			SetBoolFlagValue(flag, false);
			return false;
		}
	}
}
