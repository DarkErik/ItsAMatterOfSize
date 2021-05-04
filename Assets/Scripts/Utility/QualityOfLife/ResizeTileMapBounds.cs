using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ResizeTileMapBounds : MonoBehaviour
{
	public Tilemap bounds;
	public Tilemap resize;
	public TileBase shadingTile;

	public void Awake() {
		Destroy(this);
	}
}
