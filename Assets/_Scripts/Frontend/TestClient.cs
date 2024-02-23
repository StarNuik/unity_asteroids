using UnityEngine;
using NaughtyAttributes;
using System.Net.Sockets;
using System.Text;
using NativeWebSocket;
using JamesFrowen.SimpleWeb;
using System;

namespace Asteroids
{
	public class TestClient : MonoBehaviour
	{
		private SimpleWebClient client;
		private Message lastMessage;

		[Button]
		private async void TestServer()
		{
			client = SimpleWebClient.Create(ushort.MaxValue, 5000, Consts.TcpConfig);

			// listen for events
			client.onConnect += () => Debug.Log($"Connected to Server");
			client.onDisconnect += () => Debug.Log($"Disconnected from Server");
			client.onData += (data) => lastMessage = Message.FromSegment(data);
			client.onError += (exception) => Debug.Log($"Error because of Server, Error:{exception}");

			client.Connect(new(Consts.ServerAddress));

			// call Process to cause events to be invoked
			// Call this from inside Unity Update method so that it will process message each frame
		}

		private void Update()
		{
			if (client != null)
			{
				client?.ProcessMessageQueue();
				Debug.Log($"Last server tick: [{lastMessage.Tick}]");
			}
		}
	}
}