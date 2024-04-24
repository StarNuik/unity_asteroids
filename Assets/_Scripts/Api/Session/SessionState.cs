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

		public List<Entity> Entities = new();
		public List<Bullet> Bullets = new();
		public List<Asteroid> Asteroids = new();

		public IEnumerable<IPhysicsEntity> PhysicsEntities
			=> Bullets
				.Select(bullet => bullet as IPhysicsEntity)
				.Union(Asteroids
					.Select(asteroid => asteroid as IPhysicsEntity)
				);
	}
}