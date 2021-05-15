using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
	public static LevelData instance;

	public Sound backgroundMusic;

	public void Awake() {
		instance = this;

	}

}
