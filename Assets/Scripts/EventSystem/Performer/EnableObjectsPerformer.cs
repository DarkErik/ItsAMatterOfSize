using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectsPerformer : Performer
{
	[SerializeField] private GameObject[] gameObjects;
	[SerializeField] private bool invertBehaivior = false;
	[SerializeField] private bool invertBehaiviorOnRelease = false;
	[SerializeField] private bool cameraFocus = true;
	[SerializeField] private bool spawnParticleEffect = true;
	
	private bool enableObjects = true;

	protected override void Perform(TriggerState state) {
		if (state.just) {
			if (state.active == !invertBehaivior) {
				enableObjects = true;

				if (cameraFocus) {
					CameraController.instance.SetFocusPoints(Util.GetGameObjectPositions(gameObjects), 1, 1, EnableObjectOnReach);
				} else {
					for (int i = 0; i < gameObjects.Length; i++) {
						if (gameObjects[i] != null) {
							gameObjects[i].SetActive(true);
							if (spawnParticleEffect) Instantiate(Factory.instance.spawnEffectPrefab, gameObjects[i].transform.position, default);
						}
					}
				}
			} else {
				if (!invertBehaiviorOnRelease) return;
				enableObjects = false;

				if (cameraFocus) {
					CameraController.instance.SetFocusPoints(Util.GetGameObjectPositions(gameObjects), 1, 1, EnableObjectOnReach);
				} else {
					for (int i = 0; i < gameObjects.Length; i++) {
						if (gameObjects[i] != null) {
							gameObjects[i].SetActive(false);
							if (spawnParticleEffect) Instantiate(Factory.instance.spawnEffectPrefab, gameObjects[i].transform.position, default);
						}
					}
				}
			}
		}
	}

	private void EnableObjectOnReach(int index) {
		if (index < gameObjects.Length) {
			if (gameObjects[index] != null) {
				gameObjects[index].SetActive(enableObjects);
				if (spawnParticleEffect) Instantiate(Factory.instance.spawnEffectPrefab, gameObjects[index].transform.position, default);
			}
		}
	}
}

