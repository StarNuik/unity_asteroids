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
			if (State.Tick < State.NextAsteroid)
				return;
			
			State.NextAsteroid = State.Tick + Random.Range(Consts.AsteroidsTimerRange.x, Consts.AsteroidsTimerRange.y);

			var asteroid = entityFactory.NewEntity<Asteroid>();
			asteroid.Position = State.PlayerPosition;
			asteroid.Velocity = State.PlayerDirection * Consts.PrimaryBulletSpeed;

			Client.Pub<CreatedAsteroid>(new() { Asteroid = asteroid, });
		}
	}
}