using System;
using Asteroids.Lib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	public class AsteroidTimerService : Service
	{
		private EntityFactoryService EntityFactory;
		
		public void Inject(EntityFactoryService entityFactory)
		{
			EntityFactory = entityFactory;
		}
		
		public void Tick(Tick tick)
		{
			if (State.Tick < State.NextAsteroid)
				return;
			
			State.NextAsteroid = State.Tick + Random.Range(Consts.AsteroidsTimerRange.x, Consts.AsteroidsTimerRange.y);
			EntityFactory.NewAsteroid();
		}
	}
}