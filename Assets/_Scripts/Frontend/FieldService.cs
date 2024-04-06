using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class FieldService : MonoBehaviour
	{
		[Editor] Vector2 Size;

		public Vector3 ToWorld(Vector2 normal)
		{
			return transform.position + new Vector3(
				normal.x * Size.x,
				normal.y * Size.y,
				0f
			);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(
				transform.position + Size.ToXY0() * 0.5f,
				Size
			);
		}
	}
}
