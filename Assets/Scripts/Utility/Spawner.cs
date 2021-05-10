using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private GameObject spawnPrefab;
	[SerializeField] private float delay = 1f;

	[SerializeField] private bool initShot = false;

	private float nextSpawn = 0f;

	public void Update() {
		if (Time.time >= nextSpawn) {
			nextSpawn = Time.time + delay;
			GameObject go = Instantiate(spawnPrefab, transform.position, transform.rotation);
			if (initShot) go.GetComponent<BasicShot>().Init(true);
		}

	}
}
