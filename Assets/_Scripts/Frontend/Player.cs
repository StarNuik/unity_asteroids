using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class Player : MonoBehaviour
	{
		[Editor] Transform player;
		[Editor] Bounds field;

		private void Update()
		{
			var state = Locator.GameState;
			var from = field.min;
			var size = field.size;
			var pos = new Vector3(
				size.x * state.PlayerPosition.x,
				size.y * state.PlayerPosition.y,
				size.z
			);
			player.position = from + pos;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(field.center, field.size);
		}
	}
}