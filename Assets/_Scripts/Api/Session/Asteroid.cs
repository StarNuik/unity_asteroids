using System;
using UnityEngine;

namespace Asteroids
{
	public struct Asteroid : IColliderEntity
	{
		public Entity Entity { get; private set; }
		public float Radius { get; private set; }
		public PhysicsBody PhysicsBody { get; set; }

		public Asteroid(Entity entity, RequestAsteroid req)
		{
			Entity = entity;
			
			Radius = req.Radius;
			PhysicsBody = req.PhysicsBody;
		}
	}
}