using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConversationPathTrigger : Trigger
{
	[SerializeField] private string[] notesContaining;

	protected override void Update() {
		base.Update();

			
		SetState(ContainsAll(TextboxUI.instance.GetLastConversationPath(), notesContaining) && !TextboxUI.inConversation);
	}

	private bool ContainsAll(LinkedList<string> l, string[] s) {
		foreach (string s1 in s)
			if (!l.Contains(s1)) return false;
		return true;
	}
}
