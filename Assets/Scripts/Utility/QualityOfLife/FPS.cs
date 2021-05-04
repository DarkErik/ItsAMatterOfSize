using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Debugs FPS on the screen
/// </summary>
public class FPS : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI txt;
	private float beginningTime;
	[SerializeField] private int iterations;
	[SerializeField] private int lowFrameThreshhold = 30;
	private int iterationsLeft;
	private int lowFrames;
	private float lastFrameTime;

	public void Awake() {
		iterationsLeft = iterations;
	}

	public void Update() {
		if (1 / (Time.realtimeSinceStartup - lastFrameTime) < lowFrameThreshhold) {
			lowFrames++;
		}
		lastFrameTime = Time.realtimeSinceStartup;

		iterationsLeft--;
		if (iterationsLeft <= 0) {
			iterationsLeft = iterations;
			txt.text = "FPS: " + 1 / ((Time.realtimeSinceStartup - beginningTime) / iterations)
					+ "\nLow Frames: " + lowFrames
					+ "\nLow Frame Percent: " + Mathf.RoundToInt(lowFrames * 100 / (float)iterations)  +"%";
			beginningTime = Time.realtimeSinceStartup;
			lowFrames = 0;
		}
	}
}
