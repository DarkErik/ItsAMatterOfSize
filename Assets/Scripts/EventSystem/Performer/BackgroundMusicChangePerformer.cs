using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicChangePerformer : Performer
{
	public Sound bgMusic = Sound.BASIC_BACKGROUND_MUSIC;
	public bool useLevelDefaultInstead = false;
	public float transitionTime = 0.5f;
	public bool overrideEqualMusic = false;

	protected override void Perform(TriggerState state) {
		SoundManager.ChangeBackgrundMusic(useLevelDefaultInstead ? LevelData.instance.backgroundMusic : bgMusic, transitionTime, overrideEqualMusic);
	}
}
