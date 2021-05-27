using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// An enum, which defines the Sound to play
/// </summary>
public enum Sound {
	NONE,
	BASIC_BACKGROUND_MUSIC,
	PLAYER_FOOTSTEPS,
	PLAYER_JUMP,
	PLAYER_LAND,
	PLAYER_HURT,
	SHOT,
	SHOT_HIT,
	RELOAD,
	PROF_SPEAKINIG,
	FLY_SPEAKING
}

public static class SoundUtility {
	public static float InBetweenPlayDelay(this Sound s) {
		switch(s) {
			case Sound.PLAYER_FOOTSTEPS:
				return 0.4f;
			case Sound.PROF_SPEAKINIG:
				return 0.3f;
			case Sound.FLY_SPEAKING:
				return 0.3f;
			default:
				return 0;
		}
	}
}

/// <summary>
/// [Singelton (instance)]
/// Class which manages Sound and Audioplayback
/// Use static functions
/// </summary>
public class SoundManager : MonoBehaviour
{
	/// <summary>
	/// Singelton
	/// </summary>
	public static SoundManager instance;

	[SerializeField] private SoundSystemDictionaryEntry[] sounds;
	[SerializeField] private Transform audioSourceParent;

	private Dictionary<Sound, float> soundLastPlayed = new Dictionary<Sound, float>();

	private AudioSource currentBackgroundMusic;

	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		} else Destroy(this.gameObject);
	}


	/// <summary>
	/// Changes the Background music (looping)
	/// </summary>
	/// <param name="sound">The Sound to Play</param>
	/// <param name="transitionTime">(Optional = 0) time of the Backgroundmusic Transition (0 = instant)</param>
	/// <param name="overrideOld">(Optional = false) if true: forces a song to start over (even if its already playing)</param>
	public static void ChangeBackgrundMusic(Sound sound, float transitionTime = 0f, bool overrideOld = false) {
		instance._ChangeBackgrundMusic(sound, transitionTime, overrideOld);
	}

	/// <summary>
	/// Plays a soundclip (once)
	/// </summary>
	/// <param name="sound">The sound to play</param>
	/// <param name="randomPitch"> if true randomizes the pitch for some variation</param>
	public static void PlaySound(Sound sound, bool randomPitch) {
		instance._PlaySound(sound, randomPitch);
	}


	private void _ChangeBackgrundMusic(Sound sound, float transitionTime, bool overrideOld) {
		SoundSystemDictionaryEntry entry = GetEntry(sound);

		AudioClip clip = entry.GetRandomClip();

		if (!overrideOld && currentBackgroundMusic != null && clip == currentBackgroundMusic.clip) return;

		GameObject go = new GameObject("AudioClip: " + sound.ToString());
		go.transform.SetParent(audioSourceParent);
		//DontDestroyOnLoad(go);

		AudioSource audioSource = go.AddComponent<AudioSource>();
		audioSource.volume = entry.volume;
		audioSource.clip = clip;
		audioSource.loop = true;
		audioSource.Play();

		if (transitionTime <= 0 || currentBackgroundMusic == null) {
			if (currentBackgroundMusic != null) {
				currentBackgroundMusic.Stop();
				Destroy(currentBackgroundMusic.gameObject);
			}

			currentBackgroundMusic = audioSource;
		} else {
			StartCoroutine(TransitionBackgroundMusic(audioSource, transitionTime));
		}
	}

	public IEnumerator TransitionBackgroundMusic(AudioSource newSource, float transitionTime) {
		float startTime = Time.realtimeSinceStartup;
		float progress;

		float defaultVolumeOldSong = currentBackgroundMusic.volume;
		float defaultVolumeNewSong = newSource.volume;

		while((progress = (Time.realtimeSinceStartup - startTime) / transitionTime) < 1) {
			currentBackgroundMusic.volume = (1 - progress) * defaultVolumeOldSong;
			newSource.volume = progress * defaultVolumeNewSong;
			yield return new WaitForEndOfFrame();
		}

		currentBackgroundMusic.Stop();
		Destroy(currentBackgroundMusic.gameObject);
		currentBackgroundMusic = newSource;
		newSource.volume = defaultVolumeNewSong;
	}

	private void _PlaySound(Sound sound, bool randomPitch) {
		SoundSystemDictionaryEntry entry = GetEntry(sound);
		AudioClip c = entry.GetRandomClip();

		if (c == null || !CanPlaySoundAndMarkSoundPlayed(sound)) {
			return;
		}

		GameObject clip = new GameObject("AudioClip: " + sound.ToString());
		clip.transform.SetParent(audioSourceParent);

		AudioSource audioSource = clip.AddComponent<AudioSource>();
		if (randomPitch) audioSource.pitch = Random.Range(0.5f, 1.5f);
		audioSource.volume = entry.volume;

		clip.AddComponent<DestroyAfterTime>().time = c.length;

		audioSource.PlayOneShot(c);
	}

	private bool CanPlaySoundAndMarkSoundPlayed(Sound sound) {
		float delay = sound.InBetweenPlayDelay();
		if (delay == 0) return true;
		float lastTimePlayed;
		if (soundLastPlayed.TryGetValue(sound, out lastTimePlayed)) {
			if (lastTimePlayed + GetEntry(sound).GetFirst().length * delay <= Time.realtimeSinceStartup) {
				soundLastPlayed[sound] = Time.realtimeSinceStartup;
				return true;
			} else
				return false;
		} else {
			soundLastPlayed.Add(sound, Time.realtimeSinceStartup);
			return true;
		}
	}


	private SoundSystemDictionaryEntry GetEntry(Sound sound) {
		foreach(SoundSystemDictionaryEntry e in sounds) {
			if (e.type == sound) return e;
		}
		Debug.LogError("Unkown Sound: " + sound.ToString());
		return null;
	}
}

[System.Serializable]
public class SoundSystemDictionaryEntry {
	public Sound type;
	public AudioClip[] clips;
	[Range(0f, 1f)]
	public float volume = 0.5f;

	public AudioClip GetRandomClip() {
		return Util.GetRandomElement(clips);
	}

	public AudioClip GetFirst() {
		return clips[0];
	}
}
