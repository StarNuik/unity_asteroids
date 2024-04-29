using System;
using UnityEngine;

namespace Asteroids
{
	// Player, Spaceship, Ship, Pc, Doll, Model, Character
	public struct PlayerActor : IColliderEntity
	{
		public Entity Entity { get; private set; }
		public float Radius { get; private set; }
		public Vector2 Direction { get; set; }
		public PhysicsBody PhysicsBody { get; set; }

		public PlayerActor(Entity entity, RequestActor _)
		{
			Entity = entity;

			Radius = Consts.PlayerRadius;
			Direction = Consts.PlayerStartDirection;
			PhysicsBody = new()
			{
				Position = Consts.PlayerStartPosition,
				Velocity = Vector2.zero,
			};
		}
	}
}