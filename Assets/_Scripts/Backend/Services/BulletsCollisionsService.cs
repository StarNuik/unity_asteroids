using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
	public class BulletsCollisionsService : EntityCollisionService<Bullet>
	{
		protected override Dictionary<Entity, Bullet> Entities => State.Bullets;

		protected override void ProcessCollision(Bullet who, Entity other)
		{
			if (other.Is(State.Asteroids))
			{
				Destroy(who);
			}

			if (other.Is(State.Missiles))
			{
				Destroy(who);
			}
		}
	}
}