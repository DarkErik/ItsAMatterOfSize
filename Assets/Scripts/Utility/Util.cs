using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for all kinds of Ultity stuff
/// </summary>
public static class Util { 

	/// <summary>
	/// Returns a random element of an array
	/// </summary>
	/// <typeparam name="T">Type of the Array (Optional)</typeparam>
	/// <param name="arr">The Array</param>
	/// <returns>A random element</returns>
	public static T GetRandomElement<T>(T[] arr) {
		return arr[Random.Range(0, arr.Length)];
	}

	/// <summary>
	/// Return wether a certain Point (x, y)
	/// is in bounds of a 2D-Array with the Dimensions width & height
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns>true when the point (x, y) is in the bounds, false otherwise</returns>
	public static bool InBounds (int x, int y, int width, int height) {
		return x >= 0 && y >= 0 && x < width && y < height;
	}

	/// <summary>
	/// Returns wether a point is inside a circle
	/// </summary>
	/// <param name="point">The point</param>
	/// <param name="sphereCenter">The center Point of the Sphere</param>
	/// <param name="radius">The Sphere radius</param>
	/// <returns>Returns wether a point is inside a circle</returns>
	public static bool PointInCircle(Vector2 point, Vector2 sphereCenter, float radius) {
		return (point.x - sphereCenter.x) * (point.x - sphereCenter.x) + (point.y - sphereCenter.y) * (point.y - sphereCenter.y) < radius * radius;
	}

	/// <summary>
	/// Removes an array element at a certain position (also shortens the Array!)
	/// RETURNS A NEW ARRAY!!!!!
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr">Input Array</param>
	/// <param name="index">the index to delete</param>
	/// <returns>A new Array, without the index</returns>
	public static T[] RemoveFromArray<T>(T[] arr, int index) {
		T[] newArr = new T[arr.Length - 1];
		int offset = 0;
		for(int i = 0; i < newArr.Length; i++) {
			if (offset == 0 && index == i) offset = 1;
			newArr[i] = arr[i + offset];
		}
		return newArr;
	}

	/// <summary>
	/// Appends a certain Element at the end of an array
	/// RETURNS A NEW ARRAY
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr">The first part of the array</param>
	/// <param name="element">The new element</param>
	/// <returns>A new, connected Array</returns>
	public static T[] AppendArray<T>(T[] arr, T element) {
		T[] newArr = new T[arr.Length + 1];
		for(int i = 0; i < arr.Length; i++) {
			newArr[i] = arr[i];
		}
		newArr[arr.Length] = element;
		return newArr;
	}

	/// <summary>
	/// Appends a certain Array at the end of an array
	/// RETURNS A NEW ARRAY
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr">The first part of the array</param>
	/// <param name="elements">The second part of the Array</param>
	/// <returns>A new, connected Array</returns>
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

	/// <summary>
	/// Returns an Vector3 Array, with the positions of the input arr
	/// </summary>
	/// <param name="arr">Gameobject arr, from which the positions will be fetched</param>
	/// <returns></returns>
	public static Vector3[] GetGameObjectPositions(GameObject[] arr) {
		Vector3[] pos = new Vector3[arr.Length];
		for(int i = 0; i < arr.Length; i++) {
			pos[i] = arr[i].transform.position;
		}
		return pos;
	}

	/// <summary>
	/// Copys a string array (Complete new reference)
	/// </summary>
	/// <param name="arr"></param>
	/// <returns></returns>
	public static string[] CopyStringArray(string[] arr) {
		if (arr == null) return null;
		string[] newArr = new string[arr.Length];
		for(int i = 0; i < arr.Length; i++) {
			newArr[i] = new string(arr[i].ToCharArray());
		}
		return newArr;
	}

	public static void ShutWholePlayerDown() {
		PlayerControler.Shutdown();
	}

	public static void ShutWholePlayerDown(float time) {
		PlayerControler.Shutdown(time);
	}

	public static void WakeWholePlayerUp() {
		PlayerControler.WakeUp();
	}


}
