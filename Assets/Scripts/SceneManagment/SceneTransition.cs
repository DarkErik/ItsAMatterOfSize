using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour {
	private static ExitDir direction;
	private static SceneTransition instance;

	private static bool skipGameInTransition = true;

	[SerializeField] private Animator anim;
	[SerializeField] private float animDuration = 0.5f;

	private void Awake() {
		instance = this;

		if (!skipGameInTransition) {
			anim.SetInteger("dir", direction.toInt());
			anim.SetTrigger("open");
		} else
			skipGameInTransition = false;
	}

	public static void LoadScene(string name, ExitDir dir = ExitDir.RIGHT) {
		direction = dir;
		instance.anim.SetInteger("dir", dir.toInt());
		instance.anim.SetTrigger("close");
		instance.StartCoroutine(instance.LoadSceneAsync(name));
	}

	private IEnumerator LoadSceneAsync(string name) {
		yield return new WaitForSecondsRealtime(animDuration);
		SceneManager.LoadScene(name);
	}
}
