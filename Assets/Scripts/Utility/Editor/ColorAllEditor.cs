using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorAll))]
public class ColorAllEditor : Editor
{
	public override void OnInspectorGUI() {
		ColorAll c = (ColorAll)target;
		Color before = c.color;
		c.color = EditorGUILayout.ColorField("Color", c.color);
		if (c.color != before) {
			foreach (SpriteRenderer r in c.GetComponentsInChildren<SpriteRenderer>())
				r.color = c.color;
		}
	}
}
