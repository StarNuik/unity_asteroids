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
		public bool IsEnabled { get; set; }

		private PolledEventStream streamIn;
		private IEventStream streamMain;
		private IEventStream streamOut;

		private SessionState state;
		private DeltaService deltaService;

		public Server()
		{
			streamIn = new();
			streamMain = new EventStream();
			streamOut = new PolledEventStream();
			
			state = new();
			deltaService = new(state);

			GameLoop();
		}

		public (IPublisher miso, PolledEventStream mosi) Connect()
		{
			return (streamIn, streamOut as PolledEventStream);
		}

		private async void GameLoop()
		{
			Subscribe();

			while (true)
			{
				streamIn.Poll();

				if (IsEnabled)
				{
					streamMain.Pub<Tick>(
						Tick.New(state, streamMain, streamOut)
					);
					state.Tick++;
				}

				await Task.Delay(Consts.ServerTickMs);
			}
		}

		private void Subscribe()
		{
			deltaService.Sub(streamIn);

			PlayerControlsService.Sub(streamMain);
			PlayerPhysicsService.Sub(streamMain);
			PlayerAttackService.Sub(streamMain);
			EntityClampService.Sub(streamMain);
			BulletFactoryService.Sub(streamMain);
			AsteroidFactoryService.Sub(streamMain);
		}
	}
}