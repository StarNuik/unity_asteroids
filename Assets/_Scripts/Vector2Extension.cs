//? https://discussions.unity.com/t/whats-the-most-efficient-way-to-rotate-a-vector2-of-a-certain-angle-around-the-axis-orthogonal-to-the-plane-they-describe/98886/3
using UnityEngine;

public static class Vector2Extension {
	public static Vector2 Rotate(this Vector2 v, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
		
		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}
}