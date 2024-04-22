using System;
using Asteroids.Lib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	public class AsteroidFactoryService : Service
	{
		private EntityFactoryService entityFactory;
		
		public void Inject(EntityFactoryService entityFactory)
		{
			this.entityFactory = entityFactory;
		}
		
		public void Tick(Tick tick)
		{
			var state = tick.State;
			var client = tick.ClientStream;

			if (state.Tick < state.NextAsteroid)
				return;
			
			state.NextAsteroid = state.Tick + Random.Range(Consts.AsteroidsTimerRange.x, Consts.AsteroidsTimerRange.y);

			var asteroid = entityFactory.NewEntity<Asteroid>();
			asteroid.Position = state.PlayerPosition;
			asteroid.Velocity = state.PlayerDirection * Consts.PrimaryBulletSpeed;

			client.Pub<CreatedAsteroid>(new() { Asteroid = asteroid, });
		}
	}
}