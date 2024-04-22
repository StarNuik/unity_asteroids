using System;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class EntityClampService : Service
	{
		public void WarpEntities(Tick _)
		{
			//todo do this for all entities
			var playerPos = State.PlayerPosition;
			playerPos.x = Mathf.Repeat(playerPos.x, 1f);
			playerPos.y = Mathf.Repeat(playerPos.y, 1f);
			State.PlayerPosition = playerPos; 
		}
	}
}