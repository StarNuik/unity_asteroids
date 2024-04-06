using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids.Frontend
{
	public class PlayerView : MonoBehaviour
	{
		[Editor] Transform player;
		
		private FieldService field => Locator.Field;
		private ISubscribable server => Locator.ServerIn;

		private void Awake()
		{
			server.Sub<PlayerDelta>(PlayerTick);
		}

		private void PlayerTick(PlayerDelta msg)
		{
			Position(msg);
			Rotation(msg);
		}

		private void Position(PlayerDelta msg)
		{
			player.position = field.ToWorld(msg.Position);
		}

		private void Rotation(PlayerDelta msg)
		{
			player.rotation = Quaternion.LookRotation(msg.Direction, Vector3.forward);
		}
	}
}