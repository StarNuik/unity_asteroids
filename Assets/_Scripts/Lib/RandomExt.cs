using UnityEngine;

namespace Asteroids
{
	public class RandomExt
	{
		public static float Range(Vector2 range)
		{
			return Random.Range(range.x, range.y);
		}

		public static int Range(Vector2Int range)
		{
			return Random.Range(range.x, range.y);
		}
	}
}