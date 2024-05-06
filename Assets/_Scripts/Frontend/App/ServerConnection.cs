using Asteroids.Frontend;
using Asteroids.Lib;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids.App
{
	public class ServerConnection : MonoBehaviour
	{
		private Server server;
		private PolledEventStream serverIn;

		public ISubscribable ServerIn { get; private set; }
		public IPublisher ClientOut { get; private set; }

		public bool IsEnabled
		{
			set => ClientOut.Pub(new UpdateServer() { IsSimEnabled = value, });
		}

		public void Poll()
		{
			serverIn.Poll();
		}

		private void Awake()
		{
			var sock = new PolledEventStream();
			
			server = new();
			var conn = server.Connect();

			ServerIn = conn.mosi;
			ClientOut = conn.miso;

			serverIn = conn.mosi;
		}

		private void OnDestroy()
		{
			IsEnabled = false;
		}
	}
}
