using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.Tilemaps;

[CustomEditor(typeof(ResizeTileMapBounds))]
public class ResizeTileMapBoundsEditor : Editor
{
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		ResizeTileMapBounds instance = (ResizeTileMapBounds)target;

		if (GUILayout.Button("Copy Down")) {
			instance.resize.ClearAllTiles();
			instance.bounds.CompressBounds();
			instance.resize.size = instance.bounds.size;
			
			//instance.resize.ResizeBounds();

			for (int ix = instance.bounds.cellBounds.xMin; ix < instance.bounds.cellBounds.xMax; ix++) {
				for (int iy = instance.bounds.cellBounds.yMin; iy < instance.bounds.cellBounds.yMax; iy++) {
					Vector3Int pos = new Vector3Int(ix, iy, 0);
					TileBase tile = instance.bounds.GetTile(pos);
					if (tile != null)
						instance.resize.SetTile(pos, tile);
				}
			}
			instance.resize.CompressBounds();

		}

		if (GUILayout.Button("Remove Other Tilemap")) {
			for (int ix = instance.resize.cellBounds.xMin; ix < instance.resize.cellBounds.xMax; ix++) {
				for (int iy = instance.resize.cellBounds.yMin; iy < instance.resize.cellBounds.yMax; iy++) {
					Vector3Int pos = new Vector3Int(ix, iy, 0);
					TileBase tile = instance.resize.GetTile(pos);
					if (tile != null && tile != instance.shadingTile) {
						instance.resize.SetTile(pos, null);
					}
				}
			}
		}	
	}
}
