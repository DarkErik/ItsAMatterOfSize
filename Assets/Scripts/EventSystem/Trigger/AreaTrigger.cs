using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : Trigger
{
	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject == PlayerControler.instance.gameObject) SetState(true);
	}

	public void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject == PlayerControler.instance.gameObject) SetState(false);
	}


}
