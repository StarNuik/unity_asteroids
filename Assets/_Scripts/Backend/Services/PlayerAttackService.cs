using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using Unity.VisualScripting;
using UnityEngine;

namespace Asteroids
{
	public class PlayerAttackService : Service
	{
		public void Tick(Tick tick)
		{
			PrimaryFire();
		}

		private void PrimaryFire()
		{
			var input = State.PlayerInput;

			if (!input.PrimaryFire)
				return;
			
			var cooled = State.Tick - State.LastPrimaryFire;
			if (cooled < Consts.PrimaryAttackCooldown)
				return;
			
			State.LastPrimaryFire = State.Tick;
			Main.Pub(new RequestBullet() { PhysicsBody = BulletBody(), });
		}

		private PhysicsBody BulletBody()
		{
			return new()
			{
				Position = State.PlayerPosition,
				Velocity = State.PlayerDirection * Consts.PrimaryBulletSpeed,
			};
		}
	}
}