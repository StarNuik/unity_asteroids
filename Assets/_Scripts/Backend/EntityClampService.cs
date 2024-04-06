using System;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class EntityClampService
	{
		public static void Sub(IEventStream server)
		{
			server.Sub<Tick>(WarpEntities);
		}

		private static void WarpEntities(Tick tick)
		{
			var state = tick.State;
			
			//todo do this for all entities
			var playerPos = state.PlayerPosition;
			playerPos.x = Mathf.Repeat(playerPos.x, 1f);
			playerPos.y = Mathf.Repeat(playerPos.y, 1f);
			state.PlayerPosition = playerPos; 
		}
	}
}