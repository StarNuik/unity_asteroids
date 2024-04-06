using System.Collections;
using System.Collections.Generic;
using Asteroids;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class Tick
	{
		public SessionState State { get; private set; }
		public IPublisher ServerStream { get; private set; }
		public IPublisher ClientStream { get; private set; }

		public static Tick New(SessionState state, IPublisher server, IPublisher @out)
		{
			return new()
			{
				State = state,
				ServerStream = server,
				ClientStream = @out,
			};
		}
	}
}