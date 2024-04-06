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
			PrimaryFire(tick);
		}

		private static void PrimaryFire(Tick tick)
		{
			//todo this is *not right*
			var state = tick.State;
			var input = state.PlayerInput;
			var server = tick.ServerStream;

			if (!input.PrimaryFire)
				return;
			
			var cooled = state.Tick - state.LastPrimaryFire;
			if (cooled < Consts.PrimaryAttackCooldown)
				return;
			
			state.LastPrimaryFire = state.Tick;
			server.Pub<CreateBullet>(CreateBullet.From(tick));
		}
	}
}