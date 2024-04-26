using System;
using System.Collections.Generic;

namespace Asteroids
{
	public class AsteroidsCollisionsService : Service
	{
		private Action<List<Asteroid>, Asteroid> AsteroidDestructor;

		public void Inject(Action<List<Asteroid>, Asteroid> asteroidDestructor)
		{
			AsteroidDestructor = asteroidDestructor;
		}
		
		public void TryCollision(Collision msg)
		{
			if (!msg.Entity.Is<Asteroid>(State.Asteroids, out var asteroid))
				return;
			
			if (msg.Other.Is<Bullet>(State.Bullets))
			{
				AsteroidDestructor(State.Asteroids, asteroid);
			}
		}
	}
}