using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameObjectsExist : Trigger
{
	[SerializeField] private GameObject[] gameObjects;

	protected override void Update() {
		base.Update();

		bool success = true;
		for(int i = 0; i < gameObjects.Length; i++) {
			if (gameObjects[i] != null) success = false;
		}

		SetState(success);
	}
}
