using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class SessionState
	{
		public int Tick;

		public InputDelta PlayerInput;
		
		public Vector2 PlayerPosition;
		public Vector2 PlayerVelocity;
		public Vector2 PlayerDirection;
		public int LastPrimaryFire;

		public List<Bullet> Bullets = new();

		public SessionState(int _ = 0)
		{
			PlayerInput = new();

			PlayerPosition = Vector2.one * 0.5f;
			PlayerVelocity = Vector2.zero;
			PlayerDirection = Vector2.right;
			LastPrimaryFire = -Consts.PrimaryAttackCooldown;
			
			Tick = 0;
		}
	}
}