using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineTrigger : Trigger
{
	[SerializeField]
	private Trigger[] trigger;

	[SerializeField][TextArea(2, 8)]
	private string combination;

	[SerializeField] private float delayed = 0f;

	private LinkedList<float> timeStamps = new LinkedList<float>();
	

	protected override void Update() {
		base.Update();


		bool state = GetStateFromString();
		if (delayed == 0)
			SetState(state);
		else { 
			if ((state != currentState.active && timeStamps.Count % 2 == 0) || (state == currentState.active && timeStamps.Count % 2 == 1)) {
				timeStamps.AddLast(Time.realtimeSinceStartup + delayed);
			}
			bool again = false;
			do {
				again = false;
				foreach (float stamp in timeStamps) {
					if (Time.realtimeSinceStartup >= stamp) {
						SetState(!GetTriggerState().active);
						again = true;
						timeStamps.Remove(stamp);
						break;
					}
				}

			} while (again);

		}
		
	}


	protected bool GetStateFromString() {
		string s = combination.Substring(0);
		s = s.Replace(" ", "");
		s = s.Replace("&&", "&");
		s = s.Replace("||", "|");
		s = s.ToUpper();

		if (trigger.Length == 0 || s == "") return false;

		for(int i = 0; i < s.Length; i++) {
			if (char.IsDigit(s[i])) {
				int start = i;
				i++;
				while(char.IsDigit(s[i])) 
					i++;
				int end = i;
				i++; // Step over the Dot
				bool just = false;
				if (s[i] == 'J') {
					just = true;
					i++; // Step over the J
				}
				bool active = s[i] == 'T';
				i++; // step over the State letter
				TriggerState state = trigger[int.Parse(s.Substring(start, end - start))].GetTriggerState();
				s = s.Replace(s.Substring(start, i - start), (state.active == active && (!just || state.just)) ? "T" : "F");
				i = start + 1; //Reset i

			}
		}


		return Resolve(s);
	}

	protected bool Resolve(string s) {
		int bracketOpen = -1;
		int bracketLevel = 0;

		for (int i = 0; i < s.Length; i++) {
			if (s[i] == '(') {
				if (bracketLevel == 0) bracketOpen = i;
				bracketLevel++;
			} else if (s[i] == ')') {
				if (bracketLevel == 1) {
					string replace = s.Substring(bracketOpen, i - bracketOpen + 1);
					s = s.Replace(replace, Resolve(replace.Substring(1, replace.Length - 2)) ? "T" : "F");
					i = bracketOpen + 1;
				}
				bracketLevel--;
			}
		}
		
		int start = 0;
		bool val = ScrapeNextTruthValue(s, ref start);
		for (int i = start; i < s.Length; i++) {
			if (s[i] == '&') {
				i++;
				val = ScrapeNextTruthValue(s, ref i) && val;
				i--;
			} else if (s[i] == '|') {
				i++;
				val = ScrapeNextTruthValue(s, ref i) || val;
				i--;
			} else Debug.LogError("CRITICAL ERROR: UNKOWN TOKEN: " + s[i]);
		}
		return val;
	}

	protected bool ScrapeNextTruthValue(string s, ref int currentPos) {
		if (s[currentPos] == '!') {
			currentPos++;
			return !ScrapeNextTruthValue(s, ref currentPos);
		} else {
			bool value = s[currentPos] == 'T';
			currentPos++;
			return value;
		}
	}
	
}
