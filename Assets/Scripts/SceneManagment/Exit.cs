using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExitDir {
	RIGHT, LEFT, TOP, BOTTOM
}

public static class ExitDirUtility {
	public static int toInt(this ExitDir dir) {
		switch(dir) {
			case ExitDir.RIGHT:
				return 0;
			case ExitDir.BOTTOM:
				return 1;
			case ExitDir.LEFT:
				return 2;
			case ExitDir.TOP:
				return 3;
			default:
				return -1;
		}
	}
}

public class Exit : MonoBehaviour { 

	public static string exitSpawnName = "";
	
	
	[SerializeField] private Transform spawnPosition;
	[SerializeField] private SceneObj scene;
	[SerializeField] private string destinationExitName = "Unique Name";
	[SerializeField] private ExitDir dir = ExitDir.RIGHT;

	public void Start() {
		if (exitSpawnName != "" && exitSpawnName == name) {
			SpawnNewPlayer(spawnPosition.position);

			

			Time.timeScale = 1f;
		}
	}

	public static void SpawnNewPlayer(Vector3 spawnPosition) {
		Destroy(GameObject.Find("Player"));
		GameObject player = Instantiate(Factory.instance.playerPrefab, spawnPosition, default);
		PlayerData.instance.ApplyPlayerData(player);
		VirtualCameraController.instance.SetFollow(player.transform);
	}


	public void OnTriggerEnter2D(Collider2D collision) {
		Entity ent = collision.GetComponent<Entity>();
		if (ent != null && ent.IsPlayer()) {
			Time.timeScale = 0f;
			PlayerData.instance.UpdateCurrentData();
			exitSpawnName = destinationExitName;
			SceneTransition.LoadScene(scene.name, dir);
		}
	}

	public void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(spawnPosition.position, 0.1f);
	}

}
