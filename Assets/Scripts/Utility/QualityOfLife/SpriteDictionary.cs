using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpriteType {
	GROUND_DECO,
	HANGING_DECO,
	PLATFORMS
}

[CreateAssetMenu(fileName = "New SpriteDic", menuName = "Costum/SpriteDictionary")]
public class SpriteDictionary : ScriptableObject
{
	public DictionaryEntry[] allEntrys;

	public Object[] GetSprites(SpriteType t) {
		foreach(DictionaryEntry e in allEntrys) {
			if (e.key == t) return e.sprites;
		}
		return null;
	}
}

[System.Serializable]
public class DictionaryEntry {
	public SpriteType key;
	public Object[] sprites;
}
