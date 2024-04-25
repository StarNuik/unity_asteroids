using System;

namespace Asteroids
{
	public class BulletsTimeoutService : Service
	{
		private EntityFactoryService EntityFactory;

		public void Inject(EntityFactoryService entityFactory)
		{
			EntityFactory = entityFactory;
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
					EntityFactory.Delete(State.Bullets, bullet);
				}
			}
		}
	}
}