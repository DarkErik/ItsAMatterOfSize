using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPerformer : Performer {

	[SerializeField] private SceneObj scene;

	protected override void Perform(TriggerState state) {
		if (state.active && state.just) SceneTransition.LoadScene(scene.name);
	}
}
