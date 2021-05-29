using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerData
{
	public static PlayerData instance = new PlayerData();


	

	public PlayerData() {
		if (instance == null) instance = this;
	}


	//ENTITY
	public int hp;
	public bool dashUnlocked;
	//Data
	public static LinkedList<string> flags = new LinkedList<string>();
	public static Dictionary<string, bool> boolFlags = new Dictionary<string, bool>();


	public void ApplyPlayerData(GameObject player) {
		player.GetComponent<Entity>().SetHp(hp);
		TriggerData.flags = flags;
		TriggerData.boolFlags = boolFlags;
		PlayerControler.instance.dashUnlocked = dashUnlocked;
	}

	public void UpdateCurrentData() {
		hp = PlayerControler.instance.GetComponent<Entity>().GetHp();
		flags.Clear();
		foreach (string s in TriggerData.flags) flags.AddLast(s);
		boolFlags.Clear();
		foreach (string s in TriggerData.boolFlags.Keys) boolFlags.Add(s, TriggerData.boolFlags[s]);
		dashUnlocked = PlayerControler.instance.dashUnlocked;
	}
}
