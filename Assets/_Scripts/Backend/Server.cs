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
			Subscribe();

			while (isEnabled)
			{
				Poll();

				playerInput.Tick(state);
				playerPhysics.Tick(state);

				Send();

				await Task.Delay(Consts.ServerTickMs);
				state.Tick++;
			}
		}

		private void Subscribe()
		{
			sock.Subscribe<InputDelta>(ApplyInput);
		}

		private void Poll()
		{
			sock.Poll<InputDelta>();
		}

		private void Send()
		{
			sock.Send<PlayerDelta>(PlayerDelta.ConstructFrom(state));
		}

		private void ApplyInput(InputDelta delta)
		{
			delta.ApplyTo(state);
		}
	}
}