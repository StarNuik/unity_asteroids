using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public struct Bullet : IColliderEntity
	{
		public Entity Entity { get; private set; }
		public float Radius { get; private set; }
		public int CreationTick { get; set; }
		public PhysicsBody PhysicsBody { get; set; }

		public Bullet(Entity entity, RequestBullet req)
		{
			Entity = entity;

			Radius = Consts.BulletRadius;

			CreationTick = req.Tick;
			PhysicsBody = req.PhysicsBody;
		}
	}
}
