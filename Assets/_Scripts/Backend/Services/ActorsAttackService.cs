using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using Unity.VisualScripting;
using UnityEngine;

namespace Asteroids
{
	public class ActorsAttackService : Service
	{
		public void Tick()
		{
			foreach (var (_, actor) in State.Actors)
			{
				PrimaryFire(actor);
			}
		}

		private void PrimaryFire(PlayerActor actor)
		{
			var input = State.PlayerInput;

			if (!input.PrimaryFire)
				return;
			
			var cooled = State.Tick - State.LastPrimaryFire;
			if (cooled < Consts.PrimaryAttackCooldown)
				return;
			
			State.LastPrimaryFire = State.Tick;
			Main.Pub(new RequestBullet()
				{
					PhysicsBody = BulletBody(actor),
					Tick = State.Tick,
				}
			);
		}

		private PhysicsBody BulletBody(PlayerActor actor)
		{
			return new()
			{
				Position = actor.PhysicsBody.Position,
				Velocity = actor.Direction * Consts.PrimaryBulletSpeed,
			};
		}
	}
}