using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	public class SessionState
	{
		public int Tick;
		public int NextId = int.MinValue;

		public InputDelta PlayerInput;
		
		// public Vector2 PlayerPosition = Vector2.one * 0.5f;
		// public Vector2 PlayerDirection = Vector2.right;
		// public Vector2 PlayerVelocity;
		public int LastPrimaryFire = -Consts.PrimaryAttackCooldown;
		public int NextAsteroid = Consts.AsteroidsTimerRange.y;

		public HashSet<Entity> Entities = new();
		public Dictionary<Entity, PlayerActor> Actors = new();
		public Dictionary<Entity, Bullet> Bullets = new();
		public Dictionary<Entity, Asteroid> Asteroids = new();

		public IEnumerable<IPhysicsEntity> PhysicsEntities =>
			ColliderEntities.Cast<IPhysicsEntity>();
		
		public IEnumerable<IColliderEntity> ColliderEntities => Actors
			.Select(pair => pair.Value as IColliderEntity)
			.Union(
				Bullets.Select(pair => pair.Value as IColliderEntity)
			)
			.Union(
				Asteroids.Select(pair => pair.Value as IColliderEntity)
			)
		;

		public List<Action> QueuedDeletes = new();
	}
}