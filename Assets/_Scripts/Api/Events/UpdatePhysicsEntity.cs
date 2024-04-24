using System;
using UnityEngine;

namespace Asteroids
{
	public struct UpdatePhysicsEntity
	{
		public Entity Entity;
		public PhysicsBody PhysicsBody;

		public UpdatePhysicsEntity(IPhysicsEntity entity)
		{
			Entity = entity.Entity;
			PhysicsBody = entity.PhysicsBody;
		}
	}
}