using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public static CameraController instance;

	public Camera cam;
	//public float lowestYValueShown = -6;

	[SerializeField] private AnimationCurve focusPointTraveAnimationCurve;
	[SerializeField] private Transform attationPoint;
	private float focusTime = 1;
	private float travelTime = 1;
	private Vector3 startPos;
	private Vector3[] focusPoints;
	private int currentFocusIndex;
	private bool isMoving = false;
	private bool focusFunctionCalled = false;
	private float startTime = 0;
	private System.Action<int> onReachFocusPoint;

	private bool takeControl = true;

	private void Awake() {
		instance = this;

	}


	private void UpdateFocusAttentionPoints() {
		if (isMoving) {
			float progress = (Time.unscaledTime - startTime) / travelTime;
			Vector3 desiredPos = Vector3.Lerp(startPos, focusPoints[currentFocusIndex], focusPointTraveAnimationCurve.Evaluate(progress));

			attationPoint.position = desiredPos;


			if (progress >= 1) {
				startPos = transform.position;
				startTime = Time.unscaledTime;
				isMoving = false;
				focusFunctionCalled = false;

				//Debug.Log($"CurrentPos: {transform.position} focusPoint: {focusPoints[currentFocusIndex]}");
			}
		} else {
			float progress = (Time.unscaledTime - startTime) / focusTime;
			if (progress >= 0.5 && !focusFunctionCalled) {
				onReachFocusPoint?.Invoke(currentFocusIndex);
				focusFunctionCalled = true;
			}
			if (progress >= 1) {
				isMoving = true;
				startTime = Time.unscaledTime;
				currentFocusIndex++;
				if (currentFocusIndex >= focusPoints.Length) {
					takeControl = false;
					Util.WakeWholePlayerUp();
				}
			}
		}
	}

	public void SetFocusPoints(Vector3[] focusPoints, float travelTime = 1, float focusTime = 1, System.Action<int> onReachFocusPoint = null) {
		this.focusPoints = focusPoints;
		this.travelTime = travelTime;
		this.focusTime = focusTime;
		this.onReachFocusPoint = onReachFocusPoint;

		startPos = transform.position;
		startTime = Time.unscaledTime;
		takeControl = true;
		currentFocusIndex = 0;
		isMoving = true;

		for(int i = 0; i < this.focusPoints.Length; i++) {
			this.focusPoints[i].z = transform.position.z;
		}
		
		this.focusPoints = Util.AppendArray(this.focusPoints, transform.position);
		


		Util.ShutWholePlayerDown();
	}


	private void Update() {
		if (takeControl)
			UpdateFocusAttentionPoints();
	}


}
