using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConversationPerformer : Performer {

	[SerializeField] private Conversation conversation;

	protected override void Perform(TriggerState state) {
		if (state.just && state.active) TextboxUI.instance.StartConversation(conversation);
	}
}
