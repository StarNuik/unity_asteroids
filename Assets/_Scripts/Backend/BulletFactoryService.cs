using System;
using Asteroids.Lib;

namespace Asteroids
{
	public class BulletFactoryService : Service
	{
		private EntityFactoryService entityFactory;

		public void Inject(EntityFactoryService entityFactory)
		{
			this.entityFactory = entityFactory;
		}

		public void CreateBullet(CreateBullet _)
		{
			var bullet = entityFactory.NewEntity<Bullet>();
			bullet.Position = State.PlayerPosition;
			bullet.Velocity = State.PlayerDirection * Consts.PrimaryBulletSpeed;

			Client.Pub<BulletCreated>(new() { Bullet = bullet, });
		}
	}
}