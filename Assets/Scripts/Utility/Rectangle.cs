using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A welldefined Rectangle
/// </summary>
[System.Serializable]
public class Rectangle {
	public int x;
	public int y;
	public int width;
	public int height;

	public Rectangle(int x, int y, int width, int height) {
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	public Rectangle((int x, int y) oneEdge, (int x, int y) otherEdge) : this(new Vector2Int(oneEdge.x, oneEdge.y), new Vector2Int(otherEdge.x, otherEdge.y)) {	}

	public Rectangle(Vector2Int oneEdge, Vector2Int otherEdge) :
		this(Mathf.Min(oneEdge.x, otherEdge.x), Mathf.Min(oneEdge.y, otherEdge.y), Mathf.Abs(oneEdge.x - otherEdge.x) + 1,
			Mathf.Abs(oneEdge.y - otherEdge.y) + 1) { }

	public Rectangle(Rectangle r) : this(r.x, r.y, r.width, r.height) {	}

	public bool Collide(Rectangle rect2) {
		return x < rect2.x + rect2.width &&
		   x + width > rect2.x &&
		   y < rect2.y + rect2.height &&
		   y + height > rect2.y;
	}

	public bool Collide(LinkedList<Rectangle> rectList) {
		foreach(Rectangle r in rectList) {
			if (this.Collide(r)) return true;
		}
		return false;
	}

	public bool Collide(List<Rectangle> rectList) {
		foreach (Rectangle r in rectList) {
			if (this.Collide(r)) return true;
		}
		return false;
	}

	public bool CollideAALine(VectorPair aaLine) {
		if (aaLine.IsHoriziontal()) {
			return aaLine.first.y >= y && aaLine.first.y < y + height && !((aaLine.first.x < x && aaLine.second.x < x) || (aaLine.first.x > x + width && aaLine.second.x > x + width));
		} else {
			return aaLine.first.x >= x && aaLine.first.x < x + width  && !((aaLine.first.y < y && aaLine.second.y < y) || (aaLine.first.y > y + height && aaLine.second.y > y + height));
		}
	}

	public bool Inside(int _x, int _y) {
		return _x >= x && _y >= y && _x < x + width && _y < y + height;
	}

	public void PumpRectDimensionsUpTo(int min) {
		if (width < min) width = min;
		if (height < min) height = min;
	}

	public Rectangle GetCenterScaledRectangle(float scalar) {
		return new Rectangle(x - (int)((width / 2) * (scalar - 1)), 
			y - (int)((height / 2) * (scalar - 1)),
			width + (int)((width ) * (scalar - 1)),
			height + (int)((height ) * (scalar - 1)));
	}

	public Rectangle GetCenterPumpedUpRect(int minDimensions) {
		Rectangle newR = new Rectangle(this);
		int diff = minDimensions - newR.width;
		if (diff > 0) {
			newR.x -= Mathf.CeilToInt(diff / 2f);
			newR.width += diff;
		}
		diff = minDimensions - newR.height;
		if (diff > 0) {
			newR.y -= Mathf.CeilToInt(diff / 2f);
			newR.height += diff;
		}

		return newR;
	}

	public void ClampToRectangle(Rectangle rect) {
		ClampToRectangle(rect.x, rect.y, rect.width, rect.height);
	}

	public void ClampToRectangle(int x, int y, int width, int height) {
		if (this.x < x) {
			int diff = x - this.x;
			this.x += diff;
			this.width -= diff;
		}

		if (this.y < y) {
			int diff = y - this.y;
			this.y += diff;
			this.height -= diff;
		}

		if(this.x + this.width > x + width) {
			if (this.x > x + width) {
				this.x = x + width;
				this.width = 0;
			} else
				this.width -= this.x + this.width - (x + width);
		}

		if (this.y + this.height > y + height) {
			if (this.y > y + height) {
				this.y = y + height;
				this.height = 0;
			} else
				this.height -= this.y + this.height - (y + height);
		}
	}

	public void DrawWireRectGizmo(float heightZ = 2f) {
		Vector3 center = new Vector3((2 * x + width) / 2f, (2 * y + height) / 2f, 0);
		Vector3 size = new Vector3(width, height, heightZ);
		Gizmos.DrawWireCube(center, size);
	}

	public override string ToString() {
		return "Rectangle { " + x + " " + y + " " + width + " " + height + " }";
	}
}

[System.Serializable]
public class RectangleFloat {
	public float x;
	public float y;
	public float width;
	public float height;

	public RectangleFloat(float x, float y, float width, float height) {
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	public RectangleFloat((float x, float y) oneEdge, (float x, float y) otherEdge) : this(new Vector2(oneEdge.x, oneEdge.y), new Vector2(otherEdge.x, otherEdge.y)) { }

	public RectangleFloat(Vector2 oneEdge, Vector2 otherEdge) :
		this(Mathf.Min(oneEdge.x, otherEdge.x), Mathf.Min(oneEdge.y, otherEdge.y), Mathf.Abs(oneEdge.x - otherEdge.x) + 1,
			Mathf.Abs(oneEdge.y - otherEdge.y) + 1) { }

	public RectangleFloat(RectangleFloat r) : this(r.x, r.y, r.width, r.height) { }

	public bool Collide(Rectangle rect2) {
		return x < rect2.x + rect2.width &&
		   x + width > rect2.x &&
		   y < rect2.y + rect2.height &&
		   y + height > rect2.y;
	}

	public bool Collide(LinkedList<Rectangle> rectList) {
		foreach (Rectangle r in rectList) {
			if (this.Collide(r)) return true;
		}
		return false;
	}

	public bool Collide(List<Rectangle> rectList) {
		foreach (Rectangle r in rectList) {
			if (this.Collide(r)) return true;
		}
		return false;
	}

	public bool CollideAALine(VectorPair aaLine) {
		if (aaLine.IsHoriziontal()) {
			return aaLine.first.y >= y && aaLine.first.y < y + height && !((aaLine.first.x < x && aaLine.second.x < x) || (aaLine.first.x > x + width && aaLine.second.x > x + width));
		} else {
			return aaLine.first.x >= x && aaLine.first.x < x + width && !((aaLine.first.y < y && aaLine.second.y < y) || (aaLine.first.y > y + height && aaLine.second.y > y + height));
		}
	}

	public bool Inside(float _x, float _y) {
		return _x >= x && _y >= y && _x < x + width && _y < y + height;
	}

	public void PumpRectDimensionsUpTo(float min) {
		if (width < min) width = min;
		if (height < min) height = min;
	}

	public RectangleFloat GetCenterScaledRectangle(float scalar) {
		return new RectangleFloat(x - (float)((width / 2) * (scalar - 1)),
			y - (float)((height / 2) * (scalar - 1)),
			width + (float)((width) * (scalar - 1)),
			height + (float)((height) * (scalar - 1)));
	}

	public RectangleFloat GetCenterPumpedUpRect(float minDimensions) {
		RectangleFloat newR = new RectangleFloat(this);
		float diff = minDimensions - newR.width;
		if (diff > 0) {
			newR.x -= Mathf.CeilToInt(diff / 2f);
			newR.width += diff;
		}
		diff = minDimensions - newR.height;
		if (diff > 0) {
			newR.y -= Mathf.CeilToInt(diff / 2f);
			newR.height += diff;
		}

		return newR;
	}

	public void ClampToRectangle(Rectangle rect) {
		ClampToRectangle(rect.x, rect.y, rect.width, rect.height);
	}

	public void ClampToRectangle(float x, float y, float width, float height) {
		if (this.x < x) {
			float diff = x - this.x;
			this.x += diff;
			this.width -= diff;
		}

		if (this.y < y) {
			float diff = y - this.y;
			this.y += diff;
			this.height -= diff;
		}

		if (this.x + this.width > x + width) {
			if (this.x > x + width) {
				this.x = x + width;
				this.width = 0;
			} else
				this.width -= this.x + this.width - (x + width);
		}

		if (this.y + this.height > y + height) {
			if (this.y > y + height) {
				this.y = y + height;
				this.height = 0;
			} else
				this.height -= this.y + this.height - (y + height);
		}
	}

	public void DrawWireRectGizmo(float heightZ = 2f) {
		Vector3 center = new Vector3((2 * x + width) / 2f, (2 * y + height) / 2f, 0);
		Vector3 size = new Vector3(width, height, heightZ);
		Gizmos.DrawWireCube(center, size);
	}

	public override string ToString() {
		return "Rectangle { " + x + " " + y + " " + width + " " + height + " }";
	}
}

