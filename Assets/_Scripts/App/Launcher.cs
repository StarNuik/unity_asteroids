using Asteroids.Frontend;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.App
{
	//todo remove this hack
	[DefaultExecutionOrder(-999)]
	public class Launcher : MonoBehaviour
	{
		private Server server;
		private PolledEventStream serverIn;

		private void Awake()
		{
			var sock = new PolledEventStream();
			
			server = new();
			var conn = server.Connect();

			Locator.ServerIn = conn.mosi;
			Locator.ClientOut = conn.miso;

			serverIn = conn.mosi;
			// Locator.SessionState = new();

			server.IsEnabled = true;
		}

		private void Update()
		{
			serverIn.Poll();
		}

		private void OnDestroy()
		{
			server.IsEnabled = false;
		}
	}
}
