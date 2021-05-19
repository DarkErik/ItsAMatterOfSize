using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
	public static Factory instance;

	public GameObject playerPrefab;

	public LayerMask groundMask;
	public GameObject spawnEffectPrefab;
	public void Awake() {
		instance = this;
	}

}
