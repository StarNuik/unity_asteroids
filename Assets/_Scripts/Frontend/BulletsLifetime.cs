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

		private ISubscribable Server => Locator.ServerIn;
		private EntitiesHelperService EntitiesHelper => Locator.EntitiesHelper;

		private Dictionary<Entity, BulletObject> bullets = new();

		private void Awake()
		{
			Server.Sub<CreateBullet>(NewBullet);
			Server.Sub<UpdatePhysicsEntity>(TryUpdateBullet);
			Server.Sub<DeleteEntity>(TryDeleteBullet);
		}

		private void NewBullet(CreateBullet msg)
			=> EntitiesHelper.NewEntity(bullets, msg.Bullet, bulletPrefab);

		private void TryUpdateBullet(UpdatePhysicsEntity update)
			=> EntitiesHelper.TryUpdatePhysics(bullets, update);

		private void TryDeleteBullet(DeleteEntity msg)
			=> EntitiesHelper.TryDeleteEntity(bullets, msg);
	}
}
