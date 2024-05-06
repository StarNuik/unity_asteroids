using System;
using System.Threading;
using System.Threading.Tasks;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class Server
	{
		private bool IsSimEnabled { get; set; }

		private PolledEventStream streamIn;
		private IEventStream streamMain;
		private IEventStream streamOut;

		private SessionState state;
		private ServerServices services;

		public Server()
		{
			streamIn = new();
			streamMain = new EventStream();
			streamOut = new PolledEventStream();
			
			state = new();
			services = new();

			services.Inject(
				state,
				streamIn,
				streamMain,
				streamOut
			);
			services.Setup();

			streamIn.Sub<UpdateServer>(Reconfig);

			GameLoop();
		}

		private void Reconfig(UpdateServer update)
		{
			IsSimEnabled = update.IsSimEnabled;
		}

		public (IPublisher miso, PolledEventStream mosi) Connect()
		{
			return (streamIn, streamOut as PolledEventStream);
		}

		private async void GameLoop()
		{
			streamMain.Pub(new Init());

			while (true)
			{
				streamIn.Poll();

				if (IsSimEnabled)
				{
					streamMain.Pub(new Tick());
					streamMain.Pub(new FinishQueued());
					//todo: ponder on whether a better solution exists
					streamMain.Pub(new Sync());
					state.Tick++;
				}

				await Task.Delay(Consts.ServerTickMs);
			}
		}
	}
}