using System;
using Asteroids.Lib;

namespace Asteroids
{
	public class BulletFactoryService
	{
		public static void Sub(IEventStream server)
		{
			server.Sub<CreateBullet>(CreateBullet);
		}

		private static void CreateBullet(CreateBullet msg)
		{
			var state = msg.Tick.State;
			var client = msg.Tick.ClientStream;

			var b = new Bullet
			{
				Position = state.PlayerPosition,
				Velocity = state.PlayerDirection * Consts.PrimaryBulletSpeed,
			};

			state.Bullets.Add(b);
			client.Pub<BulletCreated>(new() { Bullet = b, });
		}
	}
}