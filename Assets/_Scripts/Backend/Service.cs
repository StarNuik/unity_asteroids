using System;
using Asteroids.Lib;

namespace Asteroids
{
	public abstract class Service
	{
		protected SessionState State { get; private set; }
		protected ISubscribable Input { get; private set; }
		protected IEventStream Main { get; private set; }
		protected IPublisher Client { get; private set; }

		public void Inject(
			SessionState state,
			ISubscribable input,
			IEventStream main,
			IPublisher client
		)
		{
			State = state;
			Input = input;
			Main = main;
			Client = client;
		}
	}
}