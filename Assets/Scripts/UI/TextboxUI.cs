using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextboxUI : MonoBehaviour
{
	public static bool inConversation = false;
	private static bool finishedCurrentMsgPrinting = false;
	private static int selectedAnswer = -1;
	public static TextboxUI instance;
	[SerializeField] private TextMeshProUGUI guiTxt;
	[SerializeField] private Image image;
	[SerializeField] private Transform answerParent;
	[SerializeField] private GameObject answerPrefab;



	public void Awake() {
		instance = this;

	}

	public void StartConversation(Conversation conversation) {
		PlayerControler.Shutdown();
		image.enabled = true;
		guiTxt.enabled = true;
		inConversation = true;
		StartCoroutine(ShowMsg(conversation));
	}

	public IEnumerator ShowMsg(Conversation conversation) {
		Conversation.ConversationNode currentNode = conversation.startNode;
		while (true) {
			if (currentNode.label.ToLower().Contains("jump")) {
				string jumpName = currentNode.label.Substring(currentNode.label.IndexOf(" ") + 1);
				Conversation.ConversationNode newNode = conversation.GetByName(jumpName);
				if (name != null) {
					currentNode = newNode;
					continue;
				}else {
					Debug.LogError("Jump Label not found: " + jumpName);
				}
			}

			foreach (string txt in currentNode.text) {
				finishedCurrentMsgPrinting = false;
				Coroutine c = StartCoroutine(PrintMsg(txt, 0.01f));
				yield return new WaitUntil(() => finishedCurrentMsgPrinting);
				yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
				yield return new WaitUntil(() => Input.GetButtonUp("Jump"));
				StopCoroutine(c);
			}
			
			if (currentNode.answers.Length == 0)
				break;
			
			if (currentNode.answers.Length == 1 && currentNode.answers[0].answerText == "") {
				currentNode = currentNode.answers[0].answerNode;
				continue;
			}

			selectedAnswer = -1;


			for(int i = 0; i < currentNode.answers.Length; i++) {
				GameObject newAwns = Instantiate(answerPrefab, answerParent);
				int v = i;
				newAwns.GetComponentInChildren<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => selectedAnswer = v));
				if (i == 0) UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(newAwns.transform.GetChild(0).gameObject);
				newAwns.GetComponentInChildren<TextMeshProUGUI>().SetText(currentNode.answers[i].answerText);
			}

			yield return new WaitUntil(() => selectedAnswer != -1);

			foreach (Transform t in answerParent)
				Destroy(t.gameObject);


			currentNode = currentNode.answers[selectedAnswer].answerNode;
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

/// <summary>
/// Class which contains a speakable conversation
/// </summary>
[System.Serializable]
public class Conversation {

	public ConversationNode startNode;

	[System.Serializable]
	public class ConversationNode {
		public string label;
		[TextArea(2, 8)]
		public string[] text;

		public ConversationAnswer[] answers;
	}

	[System.Serializable]
	public class ConversationAnswer {
		public string answerText;
		public ConversationNode answerNode;
	}

	public ConversationNode GetByName(string name) {
		return _GetByName(startNode, name);
	}

	private ConversationNode _GetByName(ConversationNode node, string name) {
		if (node.label == name) return node;
		foreach(ConversationAnswer answer in node.answers) {
			ConversationNode n = _GetByName(answer.answerNode, name);
			if (n != null) return n;
		}
		return null;
	}
}

