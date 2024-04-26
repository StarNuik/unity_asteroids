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
		
		public Vector2 PlayerPosition = Vector2.one * 0.5f;
		public Vector2 PlayerDirection = Vector2.right;
		public Vector2 PlayerVelocity;
		public int LastPrimaryFire = -Consts.PrimaryAttackCooldown;
		public int NextAsteroid = Consts.AsteroidsTimerRange.y;

		//todo: change to HashSet<>
		public List<Entity> Entities = new();
		//todo: change to Dictionary<>
		public List<Bullet> Bullets = new();
		//todo: change to Dictionary<>
		public List<Asteroid> Asteroids = new();

		public IEnumerable<IPhysicsEntity> PhysicsEntities => ColliderEntities.Cast<IPhysicsEntity>();
		
		public IEnumerable<IColliderEntity> ColliderEntities => Bullets
			.Select(bullet => bullet as IColliderEntity)
			.Union(
				Asteroids.Select(asteroid => asteroid as IColliderEntity)
			);

		public List<Action> QueuedDeletes = new();
	}
}