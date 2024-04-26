using System;
using System.Collections.Generic;

namespace Asteroids
{
	public class BulletsTimeoutService : Service
	{
		private Action<List<Bullet>, Bullet> BulletDestructor;

		public void Inject(Action<List<Bullet>, Bullet> bulletDestructor)
		{
			BulletDestructor = bulletDestructor;
		}

		public void Tick()
		{
			var bullets = State.Bullets;
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