using System;
using System.Collections.Generic;

namespace Asteroids
{
	public class MissilesCollisionsService : EntityCollisionService<Missile>
	{
		protected override Dictionary<Entity, Missile> Entities => State.Missiles;

		protected override void ProcessCollision(Missile who, Entity other)
		{
			if (other.Is<Bullet>(State.Bullets))
			{
				Destroy(who);
			}

			if (other.Is<PlayerActor>(State.Actors))
			{
				Destroy(who);
			}
		}
	}
}