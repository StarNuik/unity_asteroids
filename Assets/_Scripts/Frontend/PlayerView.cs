using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids.Frontend
{
	public class PlayerView : MonoBehaviour
	{
		[Editor] Transform player;
		[Editor] Bounds field;

		private void Awake()
		{
			var server = Locator.ServerIn;
			server.Sub<PlayerDelta>(PlayerTick);
		}

		private void PlayerTick(PlayerDelta msg)
		{
			Position(msg);
			Rotation(msg);
		}

		private void Position(PlayerDelta msg)
		{
			var from = field.min;
			var size = field.size;
			var pos = new Vector3(
				size.x * msg.Position.x,
				size.y * msg.Position.y,
				size.z
			);
			player.position = from + pos;
		}

		private void Rotation(PlayerDelta msg)
		{
			player.rotation = Quaternion.LookRotation(msg.Direction, Vector3.forward);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(field.center, field.size);
		}
	}
}