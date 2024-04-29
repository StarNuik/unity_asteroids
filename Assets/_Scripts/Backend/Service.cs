using System;
using System.Collections.Generic;
using Asteroids.Lib;

namespace Asteroids
{
	public abstract class Service
	{
		protected SessionState State { get; private set; }
		protected IPublisher Main { get; private set; }

		public void Inject(
			SessionState state,
			IPublisher main
		)
		{
			State = state;
			Main = main;
		}
	}
}