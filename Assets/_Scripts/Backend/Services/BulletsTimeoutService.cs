using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
	public class BulletsTimeoutService : Service
	{
		private Action<Dictionary<Entity, Bullet>, Bullet> BulletDestructor;

		public void Inject(Action<Dictionary<Entity, Bullet>, Bullet> bulletDestructor)
		{
			BulletDestructor = bulletDestructor;
		}

		public void Tick()
		{
			var bullets = State.Bullets.Values.ToList();
			for (int i = bullets.Count - 1; i >= 0; i--)
			{
				var bullet = bullets[i];

				var created = bullet.CreationTick;
				var deathTick = created + Consts.BulletLifeDuration;
				var ticksLeft = deathTick - State.Tick;

				if (ticksLeft <= 0)
				{
					BulletDestructor(State.Bullets, bullet);
				}
			}
		}
	}
}