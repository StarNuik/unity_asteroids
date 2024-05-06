using System;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	public class MissilesMovementService : Service
	{
		public void Tick()
		{
			var entities = State.Missiles.Keys.ToList();
			foreach (var entity in entities)
			{
				var missile = State.Missiles[entity];
				missile = Movement(missile);
				State.Missiles[entity] = missile;
			}
		}

		private Missile Movement(Missile missile)
		{
			// ECS-world problems?
			if (State.Actors.Count <= 0)
				return missile;
			
			var actor = State.Actors.First().Value;
			var body = missile.PhysicsBody;

			var towardsPlayer = (actor.PhysicsBody.Position - body.Position).normalized;
			var vel = body.Velocity;
			var preferred = towardsPlayer * Consts.MissileTopSpeed;
			body.Velocity = Vector2.Lerp(vel, preferred, Consts.MissileVelocityLerpFactor);

			missile.PhysicsBody = body;
			return missile;
		}
	}
}