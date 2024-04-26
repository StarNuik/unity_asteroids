using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	// duplicates are sent on purpose
	public class CollisionsService : Service
	{
		//maybe: space partitioning (grid)
		public void Tick()
		{
			var colliders = State.ColliderEntities;
			foreach (var collider in colliders)
			{
				foreach (var other in colliders)
				{
					if (other == collider)
						continue;
					
					TestCollisionPair(collider, other);
				}
			}
		}

		//todo: cache sqrRadii
		private void TestCollisionPair(IColliderEntity collider, IColliderEntity other)
		{
			if (collider.Entity == other.Entity)
				return;
			
			var diff = collider.PhysicsBody.Position - other.PhysicsBody.Position;
			var sqrDistance = Vector2.SqrMagnitude(diff);
			var sqrRadius = collider.Radius * collider.Radius + other.Radius * other.Radius;

			if (sqrDistance > sqrRadius)
				return;
			
			Main.Pub<Collision>(new() { Entity = collider.Entity, Other = other.Entity});
		}
	}
}