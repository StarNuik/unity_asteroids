using System;
using UnityEngine;

namespace Asteroids
{
	public static class MathfExt
	{
		public static float InverseLerp(Vector2 range, float value)
		{
			return Mathf.InverseLerp(range.x, range.y, value);
		}
		
		public static float Lerp(Vector2 range, float time)
		{
			return Mathf.Lerp(range.x, range.y, time);
		}
	}
}