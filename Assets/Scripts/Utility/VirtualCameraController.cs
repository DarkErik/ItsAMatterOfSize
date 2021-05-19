using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
	public static VirtualCameraController instance;

	public Cinemachine.CinemachineVirtualCamera cam;

	public void Awake() {
		instance = this;
	}

	public void SetFollow(Transform follow) {
		cam.Follow = follow;
	}
}
