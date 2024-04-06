using System;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class BulletsLifetime : MonoBehaviour
	{
		[Editor] BulletObject bulletPrefab;

		private ISubscribable server => Locator.ServerIn;
		private FieldService field => Locator.Field;

		private void Awake()
		{
			server.Sub<BulletCreated>(NewBullet);
		}

		private void NewBullet(BulletCreated msg)
		{
			var clone = Instantiate(bulletPrefab);
			clone.Position = field.ToWorld(msg.Bullet.Position);
			clone.Velocity = msg.Bullet.Velocity.ToXY0();
			// clone.
		}
	}
}
