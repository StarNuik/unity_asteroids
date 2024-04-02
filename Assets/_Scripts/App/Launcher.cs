using Asteroids.Frontend;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.App
{
	public class Launcher : MonoBehaviour
	{
		private Server server;

		private void Awake()
		{
			var sock = new ThreadSocket();
			
			server = new();
			server.Enable(sock);

			Locator.Socket = sock;
			Locator.SessionState = new();
		}

		private void OnDestroy()
		{
			server.Disable();
		}
	}
}
