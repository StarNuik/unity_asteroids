using System;
using UnityEngine;

namespace Asteroids
{
	public struct Asteroid : IPhysicsEntity
	{
		public Entity Entity { get; private set; }
		public PhysicsBody PhysicsBody { get; set; }

		public Asteroid(Entity entity)
		{
			Entity = entity;
			PhysicsBody = default;
		}
	}
}