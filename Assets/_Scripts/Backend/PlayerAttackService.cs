using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class PlayerAttackService
	{
		public static void Sub(IEventStream mainStream)
		{
			mainStream.Sub<Tick>(Tick);
		}

		private static void Tick(Tick tick)
		{
			var state = tick.State;
			var input = state.PlayerInput;

			PrimaryFire(state, input);
		}

		private static void PrimaryFire(SessionState state, InputDelta input)
		{
			if (!input.PrimaryFire)
				return;
			
			var cooled = state.Tick - state.LastPrimaryFire;
			if (cooled < Consts.PrimaryAttackCooldown)
				return;
			
			state.LastPrimaryFire = state.Tick;
			// state.Bullets.Add();
		}

		// private static Bullet BulletFactory(SessionState state)
		// {
		// 	var b = new Bullet();
		// 	b.Position = state.PlayerPosition;
		// 	b.Velocity = state.PlayerDirection * Consts.PrimaryBulletSpeed;
		// 	return b;
		// }
	}
}