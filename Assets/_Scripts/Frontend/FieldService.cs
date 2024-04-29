using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class FieldService : MonoBehaviour
	{
		[Editor] float Size;

		public Vector3 ToWorld(Vector2 normal)
		{
			return transform.position + new Vector3(
				normal.x * Size,
				normal.y * Size,
				0f
			);
		}

		public Vector3 TransformVector(Vector2 vector)
		{
			return vector.ToXY0() * Size;
		}

		public float TransformDimension(float dim)
		{
			return dim * Size;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(
				transform.position + Vector2.one.ToXY0() * Size * 0.5f,
				Vector2.one.ToXY0() * Size
			);
		}
	}
}
