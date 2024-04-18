using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class SessionState
	{
		public int Tick;

		public InputDelta PlayerInput;
		
		public Vector2 PlayerPosition = Vector2.one * 0.5f;
		public Vector2 PlayerDirection = Vector2.right;
		public Vector2 PlayerVelocity;
		public int LastPrimaryFire = -Consts.PrimaryAttackCooldown;

		public List<Bullet> Bullets = new();

		public int NextAsteroid = Consts.AsteroidsTimerRange.y;
		public List<Asteroid> Asteroids = new();
	}
}