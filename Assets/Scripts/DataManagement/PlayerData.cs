using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
	public static PlayerData instance = new PlayerData();

	public PlayerData() {
		if (instance == null) instance = this;
	}


	//ENTITY
	public int hp;


	public void ApplyPlayerData(GameObject player) {
		player.GetComponent<Entity>().SetHp(hp);


	}

	public void UpdateCurrentData() {
		hp = PlayerControler.instance.GetComponent<Entity>().GetHp();
	}
}
