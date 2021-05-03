using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util { 
	public static T GetRandomElement<T>(T[] arr) {
		return arr[Random.Range(0, arr.Length)];
	}


	public static bool InBounds (int x, int y, int width, int height) {
		return x >= 0 && y >= 0 && x < width && y < height;
	}

	public static bool PointInCircle(Vector2 point, Vector2 sphereCenter, float radius) {
		return (point.x - sphereCenter.x) * (point.x - sphereCenter.x) + (point.y - sphereCenter.y) * (point.y - sphereCenter.y) < radius * radius;
	}

	public static T[] RemoveFromArray<T>(T[] arr, int index) {
		T[] newArr = new T[arr.Length - 1];
		int offset = 0;
		for(int i = 0; i < newArr.Length; i++) {
			if (offset == 0 && index == i) offset = 1;
			newArr[i] = arr[i + offset];
		}
		return newArr;
	}

	public static T[] AppendArray<T>(T[] arr, T element) {
		T[] newArr = new T[arr.Length + 1];
		for(int i = 0; i < arr.Length; i++) {
			newArr[i] = arr[i];
		}
		newArr[arr.Length] = element;
		return newArr;
	}

	public static T[] AppendFirstArray<T>(T[] arr, params T[] elements) {
		if (arr == null) return elements;

		T[] newArr = new T[arr.Length + elements.Length];
		for (int i = 0; i < arr.Length; i++) {
			newArr[i + elements.Length] = arr[i];
		}
		for (int i = 0; i < elements.Length; i++) {
			newArr[i] = elements[i];
		}
		return newArr;
	}

	public static Vector3[] GetGameObjectPositions(GameObject[] arr) {
		Vector3[] pos = new Vector3[arr.Length];
		for(int i = 0; i < arr.Length; i++) {
			pos[i] = arr[i].transform.position;
		}
		return pos;
	}

	public static string[] CopyStringArray(string[] arr) {
		if (arr == null) return null;
		string[] newArr = new string[arr.Length];
		for(int i = 0; i < arr.Length; i++) {
			newArr[i] = new string(arr[i].ToCharArray());
		}
		return newArr;
	}


}
