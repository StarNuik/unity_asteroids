using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
namespace Asteroids
{
	public class Server
	{
		private bool isEnabled;
		private Socket sock;
		private GameSim game;

		public void Enable(Socket socket)
		{
			isEnabled = true;
			sock = socket;

			game = new();
			MainLoop();
		}

		public void Disable()
		{
			isEnabled = false;
		}

		private async void MainLoop()
		{
			while (isEnabled)
			{
				await Task.Delay(Consts.ServerTickMs);
				
				Receive();
				game.Tick();
				Send();
			}	
		}

		private void Send()
		{
			lock (sock)
			{
				sock.PlayerPosition = game.State.PlayerPosition;
			}
		}

		private void Receive()
		{
			InputMap? localDelta = null;
			lock (sock)
			{
				if (sock.InputDelta.HasValue)
				{
					localDelta = sock.InputDelta.Value;
					sock.InputDelta = null;
				}
			}

			if (localDelta.HasValue)
			{
				game.PlayerInput = localDelta.Value;
			}
		}
	}
}