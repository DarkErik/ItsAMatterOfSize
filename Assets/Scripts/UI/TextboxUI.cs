using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextboxUI : MonoBehaviour
{
	public static bool inConversation = false;
	private static bool finishedCurrentMsgPrinting = false;
	public static TextboxUI instance;
	[SerializeField] private TextMeshProUGUI guiTxt;
	[SerializeField] private Image image;


	public void Start() {
		instance = this;
		StartConversation(new string[] { "Hello Darkness my old firend,", "Ive come to talk to you again", "But the visions softly recreate" });
	}

	public void StartConversation(string[] txt) {
		PlayerControler.Shutdown();
		image.enabled = true;
		guiTxt.enabled = true;
		inConversation = true;
		StartCoroutine(ShowMsg(txt));
	}

	public IEnumerator ShowMsg(string[] args) {
		for(int i = 0; i < args.Length; i++) {
			finishedCurrentMsgPrinting = false;
			Coroutine c = StartCoroutine(PrintMsg(args[i], 0.01f));
			yield return new WaitUntil(() => finishedCurrentMsgPrinting);
			yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
			yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
			StopCoroutine(c);
		}
		PlayerControler.WakeUp();

		image.enabled = false;
		guiTxt.enabled = false;

		inConversation = false;
	}

	public IEnumerator PrintMsg(string msg, float delay) {
		for(int i = 0; i < msg.Length; i++) {
			guiTxt.text = msg.Substring(0, i + 1);
			yield return new WaitForSeconds(delay);
			if (Input.GetButtonDown("Jump")) {
				guiTxt.text = msg;
				yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
				break;
			}
		}
		finishedCurrentMsgPrinting = true;
	}
}
