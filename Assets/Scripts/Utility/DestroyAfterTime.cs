using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	public float time = 1f;

	public void Start() {
		Destroy(this.gameObject, time);
	}
}
