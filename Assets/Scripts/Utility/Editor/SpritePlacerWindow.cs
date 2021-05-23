using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpritePlacerWindow : EditorWindow {

	enum SpriteLayer {
		BACKGROUND,
		FOREGROUND,
		BLACK_FOREGROUND
	}

	private SpriteDictionary dic;
	private SpriteType type = SpriteType.GROUND_DECO;
	private bool activated = true;
	private GameObject nextObject;
	private SpriteLayer spriteLayer = SpriteLayer.BACKGROUND;
	private Color color = Color.white;
	private float baseScale = 1f;
	private float scaleDiffrence = 0f;
	private bool seperateScaleAxis = false;
	private float rotation = 0;
	private bool flipRandomX = true;



	private bool mouseInSceneView = false;

	[MenuItem("Custom/Sprite Placer Window")]
	public static void Init() {
		EditorWindow w = EditorWindow.GetWindow<SpritePlacerWindow>();
		w.Show();
	}

	public void OnEnable() {
		SceneView.duringSceneGui -= CustomUpdate;
		SceneView.duringSceneGui += CustomUpdate;
	}

	public void OnDisable() {
		SceneView.duringSceneGui -= CustomUpdate;
		
	}

	public void OnGUI() {
		activated = EditorGUILayout.Toggle("Activate", activated);
		dic = (SpriteDictionary) EditorGUILayout.ObjectField("Sprite Dictionary", dic, typeof(SpriteDictionary), false);

		if (dic == null) {
			SpriteDictionary[] dicArr = Resources.FindObjectsOfTypeAll<SpriteDictionary>();
			if (dicArr != null && dicArr.Length > 0) dic = dicArr[0];
		}


		type = (SpriteType)EditorGUILayout.EnumPopup("Type", type);
		spriteLayer = (SpriteLayer)EditorGUILayout.EnumPopup("Layer", spriteLayer);
		color = EditorGUILayout.ColorField(color);
		baseScale = EditorGUILayout.FloatField("Base Scale", baseScale);
		scaleDiffrence = EditorGUILayout.Slider("Scale Diffrence", scaleDiffrence, 0, 1);
		seperateScaleAxis = EditorGUILayout.Toggle("Seperate Scale Axis", seperateScaleAxis);
		rotation = EditorGUILayout.Slider("Rotation", rotation, 0, 360);
		flipRandomX = EditorGUILayout.Toggle("Flip Random X", flipRandomX);
	}

	public void CustomUpdate(SceneView view) {
		if (!activated || dic == null) return;

		if (!mouseInSceneView) {
			if (Event.current.type == EventType.MouseEnterWindow) mouseInSceneView = true;
			else return;
		}


		if (nextObject == null) {
			Object obj = Util.GetRandomElement(dic.GetSprites(type));
			SpriteRenderer s;
			if (obj is Sprite) {
				nextObject = new GameObject("Preview");
				s = nextObject.AddComponent<SpriteRenderer>();
				s.sprite = (Sprite)obj;
			} else {
				nextObject = (GameObject)PrefabUtility.InstantiatePrefab(obj);
				nextObject.name = "Preview";
				s = nextObject.GetComponent<SpriteRenderer>();
			}
			

			if (seperateScaleAxis)
				nextObject.transform.localScale = new Vector3(baseScale * Random.Range(1 - scaleDiffrence, 1 + scaleDiffrence), baseScale * Random.Range(1 - scaleDiffrence, 1 + scaleDiffrence), 1f);
			else {
				float scale = baseScale * Random.Range(1 - scaleDiffrence, 1 + scaleDiffrence);
				nextObject.transform.localScale = new Vector3(scale, scale, 1f);
			}
			nextObject.transform.rotation = Quaternion.Euler(0f, 0f, rotation);


			s.color = new Color(color.r, color.g, color.b, .5f);
			if (flipRandomX) s.flipX = Random.value > 0.5f;

			SetObjectToCurrentLayer();
		}

		Event e = Event.current;
		switch(e.type) {
			case EventType.MouseMove:
				Camera cam = Camera.current;
				Vector3 newPos = cam.ScreenToWorldPoint(new Vector2(e.mousePosition.x, cam.pixelHeight -e.mousePosition.y));
				newPos.z = 0;
				nextObject.transform.position = newPos;

				e.Use();

				break;
			case EventType.MouseDown:
				if (e.button != 0) break;

				GameObject parent = null;

				switch(spriteLayer) {
					case SpriteLayer.BACKGROUND:
						parent = GameObject.Find("BackgroundObjects");
						break;
					case SpriteLayer.FOREGROUND:
						parent = GameObject.Find("ForeGroundObjects");
						break;
					case SpriteLayer.BLACK_FOREGROUND:
						parent = GameObject.Find("Black Foreground");
						break;
				}

				nextObject.transform.parent = parent.transform;
				nextObject.name = "Scene Object " + type;
				nextObject.GetComponent<SpriteRenderer>().color = color;

				Undo.RegisterCreatedObjectUndo(nextObject, "Created Scene Object");

				nextObject = null;

				e.Use();
				break;

			case EventType.MouseLeaveWindow:
				mouseInSceneView = false;
				DestroyImmediate(nextObject);
				break;

			case EventType.KeyDown:
				switch(e.keyCode) {
					case KeyCode.R:
						rotation = (rotation + 90) % 360;
						nextObject.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
						break;
					case KeyCode.Q:
						DestroyImmediate(nextObject);
						break;
				}
				e.Use();
				break;

		}
	}

	private void SetObjectToCurrentLayer() {
		int sortingLayer = -1;
		switch (spriteLayer) {
			case SpriteLayer.BACKGROUND:
				sortingLayer = SortingLayer.NameToID("Background Objects");
				break;
			case SpriteLayer.FOREGROUND:
				sortingLayer = SortingLayer.NameToID("Foreground Objects");
				break;
			case SpriteLayer.BLACK_FOREGROUND:
				sortingLayer = SortingLayer.NameToID("Black Foreground");
				break;
		}
		
		nextObject.GetComponent<SpriteRenderer>().sortingLayerID = sortingLayer;
	}
}
