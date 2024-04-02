using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids.Frontend
{
	public class PlayerView : MonoBehaviour
	{
		[Editor] Transform player;
		[Editor] Bounds field;

		private SessionState state => Locator.SessionState;

		private void Update()
		{
			Position();
			Rotation();
		}

		private void Position()
		{
			var from = field.min;
			var size = field.size;
			var pos = new Vector3(
				size.x * state.PlayerPosition.x,
				size.y * state.PlayerPosition.y,
				size.z
			);
			player.position = from + pos;
		}

		private void Rotation()
		{
			player.rotation = Quaternion.LookRotation(state.PlayerDirection, Vector3.forward);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(field.center, field.size);
		}
	}
}