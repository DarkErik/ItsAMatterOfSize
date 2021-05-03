using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VectorPair {
	public Vector3 first, second;
	public bool positiveDirection = true;

	public VectorPair(Vector3 first, Vector3 second) {
		this.first = first;
		this.second = second;
	}

	public VectorPair(Vector3 same) {
		this.first = same;
		this.second = same;
	}

	public VectorPair() {

	}

	public bool IsDefault(){
		return first == Vector3.zero && second == Vector3.zero;
	}


	public bool IsHoriziontal() {
		return first.y  == second.y;
	}
}
