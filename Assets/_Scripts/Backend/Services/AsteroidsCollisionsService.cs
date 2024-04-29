using System;
using System.Collections.Generic;

namespace Asteroids
{
	public class AsteroidsCollisionsService : Service
	{
		private Action<Dictionary<Entity, Asteroid>, Asteroid> AsteroidDestructor;

		public void Inject(Action<Dictionary<Entity, Asteroid>, Asteroid> asteroidDestructor)
		{
			AsteroidDestructor = asteroidDestructor;
		}
		
		public void TryCollision(Collision msg)
		{
			if (!msg.Entity.Is<Asteroid>(State.Asteroids, out var asteroid))
				return;
			
			if (msg.Other.Is<Bullet>(State.Bullets))
				WithBullet(asteroid);
		}

		private void WithBullet(Asteroid asteroid)
		{
			AsteroidDestructor(State.Asteroids, asteroid);
		}
	}
}