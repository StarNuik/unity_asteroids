using System;
using System.Collections.Generic;
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

		private Dictionary<Entity, BulletObject> bullets = new();

		private void Awake()
		{
			server.Sub<CreateBullet>(NewBullet);
			server.Sub<UpdatePhysicsEntity>(TryUpdateBullet);
			server.Sub<DeleteEntity>(TryDeleteBullet);
		}

		private void TryDeleteBullet(DeleteEntity msg)
		{
			if (!bullets.ContainsKey(msg.Entity))
				return;
			
			bullets.Remove(msg.Entity, out var bullet);
			Destroy(bullet.gameObject);
		}

		private void NewBullet(CreateBullet msg)
		{
			var bullet = msg.Bullet;

			var instance = Instantiate(bulletPrefab);
			instance.Position = field.ToWorld(bullet.PhysicsBody.Position);
			instance.Velocity = bullet.PhysicsBody.Velocity.ToXY0();

			bullets.Add(bullet.Entity, instance);
			// clone.
		}

		private void TryUpdateBullet(UpdatePhysicsEntity update)
		{
			var entity = update.Entity;
			if (!bullets.ContainsKey(entity))
				return;
			
			var bullet = bullets[entity];
			bullet.Position = field.ToWorld(update.PhysicsBody.Position);
			bullet.Velocity = update.PhysicsBody.Velocity.ToXY0();
		}
	}
}
