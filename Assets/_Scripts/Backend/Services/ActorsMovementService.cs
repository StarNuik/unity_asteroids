using System.Linq;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Backend
{
	public class ActorsMovementService : Service
	{
		private new InputDelta Input => State.PlayerInput;

		public void Tick()
		{
			var entities = State.Actors.Keys.ToList();
			foreach (var entity in entities)
			{
				var actor = State.Actors[entity];
				actor = Direction(actor);
				actor = Acceleration(actor);
				actor = Drag(actor);
				State.Actors[entity] = actor;
			}
		}

		private PlayerActor Direction(PlayerActor actor)
		{
			if (Input.Rotate == 0)
				return actor;
			
			var dir = actor.Direction;
			var deltaAngle =
				Consts.PlayerAngularSpeed
				* Input.Rotate
				* Consts.ServerDeltaTime;
			
			actor.Direction = dir.RotateDegrees(deltaAngle);
			return actor;
		}

		private PlayerActor Acceleration(PlayerActor actor)
		{
			if (!Input.Accelerate)
				return actor;
			
			var body = actor.PhysicsBody;
			
			var vel = body.Velocity;
			var dir = actor.Direction;
			var add = dir * Consts.PlayerAcceleration * Consts.ServerDeltaTime;
			body.Velocity = Vector2.ClampMagnitude(vel + add, Consts.PlayerTopSpeed);

			actor.PhysicsBody = body;
			return actor;
		}

		private PlayerActor Drag(PlayerActor actor)
		{
			var body = actor.PhysicsBody;
			
			var vel = body.Velocity;
			var dir = vel.normalized;
			var len = vel.magnitude;
			var next = Mathf.Max(0f, len - Consts.WorldDrag * Consts.ServerDeltaTime);
			body.Velocity = dir * next;
			
			actor.PhysicsBody = body;
			return actor;
		}
	}
}