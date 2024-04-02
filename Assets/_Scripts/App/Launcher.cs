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
			var sock = new PolledEventStream();
			
			server = new();
			var conn = server.Connect();

			Locator.StreamIn = conn.mosi;
			Locator.StreamOut = conn.miso;
			Locator.SessionState = new();

			server.IsEnabled = true;
		}

		private void OnDestroy()
		{
			server.IsEnabled = false;
		}
	}
}
