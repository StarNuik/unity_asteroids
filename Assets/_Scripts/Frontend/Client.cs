using UnityEngine;

namespace Asteroids
{
	public class Client : MonoBehaviour
	{
		private void Update()
		{
			var state = Locator.GameState;
			var sock = Locator.Socket;
			var input = Locator.ClientInput;
			lock (sock)
			{
				if (sock.PlayerPosition.HasValue)
				{
					state.PlayerPosition = sock.PlayerPosition.Value;
				}

				if (input.InputDelta.HasValue)
				{
					sock.InputDelta = input.InputDelta.Value;
					input.InputDelta = null;
				}
			}
		}
	}
}