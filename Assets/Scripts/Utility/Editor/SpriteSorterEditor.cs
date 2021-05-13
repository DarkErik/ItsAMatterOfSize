using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteSorter))]
public class SpriteSorterEditor : Editor
{
	public override void OnInspectorGUI() {
		SpriteSorter sorter = (SpriteSorter)target;

		SpriteRenderer renderer = sorter.GetComponent<SpriteRenderer>();

		sorter.isChild = EditorGUILayout.Toggle("Is Child", sorter.isChild);
		sorter.layer = EditorGUILayout.IntField(sorter.isChild ? "Offset from Base" : "Layer", sorter.layer);

		if (GUILayout.Button("Move Up")) sorter.layer++;
		if (GUILayout.Button("Move Down")) sorter.layer--;

		if (!sorter.isChild) {
			if (GUILayout.Button("Randomize Color")) {
				renderer.color = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
			}
		}

		sorter.UpdateSortingLayer();
	}

	
}
