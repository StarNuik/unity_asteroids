using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	//rules: prefer `LastSmth` over `NextSmth`
	public class SessionState
	{
		public int Tick;

		public int LastId = int.MinValue;
		public int LastPrimaryFire = -Consts.PrimaryAttackCooldown;
		public int NextAsteroid = Consts.AsteroidsTimerRange.y;
		public int LastMissile = 0;
		public int PlayerScore = 0;
		
		public UpdateInput PlayerInput;

		public HashSet<Entity> Entities = new();
		public Dictionary<Entity, PlayerActor> Actors = new();
		public Dictionary<Entity, Bullet> Bullets = new();
		public Dictionary<Entity, Asteroid> Asteroids = new();
		public Dictionary<Entity, Missile> Missiles = new();

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
			.Union(
				Missiles.Select(pair => pair.Value as IColliderEntity)
			)
		;

		public List<Action> QueuedDeletes = new();
	}
}