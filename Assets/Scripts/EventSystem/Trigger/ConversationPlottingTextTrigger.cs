using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPlottingTextTrigger : Trigger
{
	protected override void Update() {
		base.Update();
		SetState(TextboxUI.isPlottingText);
	}

}
