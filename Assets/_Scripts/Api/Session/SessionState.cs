using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Asteroids
{
	public class SessionState
	{
		public int Tick;
		public int NextId = int.MinValue;

		public List<IEntity> Entities = new();

		public InputDelta PlayerInput;
		
		public Vector2 PlayerPosition = Vector2.one * 0.5f;
		public Vector2 PlayerDirection = Vector2.right;
		public Vector2 PlayerVelocity;
		public int LastPrimaryFire = -Consts.PrimaryAttackCooldown;
		public int NextAsteroid = Consts.AsteroidsTimerRange.y;

		public ReadOnlyCollection<IPhysicsEntity> PhysicsEntities;
		public ReadOnlyCollection<Bullet> Bullets;
		public ReadOnlyCollection<Asteroid> Asteroids;
	}
}