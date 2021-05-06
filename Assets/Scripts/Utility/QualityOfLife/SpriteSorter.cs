using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
	public static readonly int LAYER_SIZE = 10;

	public int layer;
	public bool isChild = true;


	public void UpdateSortingLayer() {
		if (!isChild) {
			GetComponent<SpriteRenderer>().sortingOrder = layer * SpriteSorter.LAYER_SIZE;
			foreach(SpriteSorter s in GetBases(this)) {
				if (s.isChild) s.UpdateSortingLayer();
			}
			
		} else
			GetComponent<SpriteRenderer>().sortingOrder = GetBase(this).layer * SpriteSorter.LAYER_SIZE + layer;
	}

	private SpriteSorter GetBase(SpriteSorter s) {
		Transform t = s.transform;
		while (t.parent != null) {
			t = t.parent;
		}

		return t.GetComponentInChildren<SpriteSorter>();
	}

	private SpriteSorter[] GetBases(SpriteSorter s) {
		Transform t = s.transform;
		while (t.parent != null) {
			t = t.parent;
		}

		return t.GetComponentsInChildren<SpriteSorter>();
	}
}
