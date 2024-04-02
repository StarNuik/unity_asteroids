using System;
using System.Threading;
using System.Threading.Tasks;
using Asteroids.Backend;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class Server
	{
		private bool isEnabled;
		private IEventSocket sock;

		private SessionState state = new(1);
		private PlayerPhysicsService playerPhysics = new();
		private PlayerInputService playerInput = new();

		public void Enable(IEventSocket socket)
		{
			isEnabled = true;
			sock = socket;

			GameLoop();
		}

		public void Disable()
		{
			isEnabled = false;
		}

		private async void GameLoop()
		{
			playerInput.Subscribe(sock);

			while (isEnabled)
			{
				await Task.Delay(Consts.ServerTickMs);

				sock.Poll<InputMap>();

				playerInput.Tick(ref state);
				playerPhysics.Tick(ref state);

				//TODO remove this hack
				sock.Send(state);

				state.Tick++;
			}	
		}
	}
}