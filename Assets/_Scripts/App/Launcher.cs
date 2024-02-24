using UnityEngine;

namespace Asteroids
{
	public class Launcher : MonoBehaviour
	{
		private Server server;

		private void Start()
		{
			var sock = new Socket();
			Locator.Socket = sock;
			
			server = new();
			server.Enable(sock);

			Locator.GameState = new();
		}
	}
}
