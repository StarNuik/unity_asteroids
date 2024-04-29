using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
	public class BulletsCollisionsService : Service
	{
		private Action<Dictionary<Entity, Bullet>, Bullet> BulletDestructor;

		public void Inject(Action<Dictionary<Entity, Bullet>, Bullet> bulletDestructor)
		{
			BulletDestructor = bulletDestructor;
		}

		public void TryCollision(Collision msg)
		{
			if (!msg.Entity.Is<Bullet>(State.Bullets, out var bullet))
				return;

			if (msg.Other.Is<Asteroid>(State.Asteroids))
			{
				BulletDestructor(State.Bullets, bullet);
			}
		}
	}
}