using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public struct Bullet : IPhysicsEntity
	{
		public Entity Entity { get; private set; }
		public PhysicsBody PhysicsBody { get; set; }
		public int CreationTick { get; set; }

		public Bullet(Entity entity, int creationTick)
		{
			Entity = entity;
			PhysicsBody = default;
			CreationTick = creationTick;
		}
	}
}
