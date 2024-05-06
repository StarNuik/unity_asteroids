using System;
using System.Collections.Generic;

namespace Asteroids
{
	public class AsteroidsCollisionsService : EntityCollisionService<Asteroid>
	{
		protected override Dictionary<Entity, Asteroid> Entities => State.Asteroids;

		protected override void ProcessCollision(Asteroid who, Entity other)
		{
			if (other.Is(State.Bullets))
			{
				Destroy(who);
			}

			if (other.Is(State.Actors))
			{
				Destroy(who);
			}
		}
	}
}