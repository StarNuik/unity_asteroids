using System;
using System.Collections.Generic;
using Asteroids.Lib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	public class AsteroidTimedSpawnService : Service
	{
		public void Tick()
		{
			if (State.Tick < State.NextAsteroid)
				return;
			
			State.NextAsteroid = State.Tick + RandomExt.Range(Consts.AsteroidsTimerRange);

			var body = new PhysicsBody();
			body.Position = ChoosePosition();
			body.Velocity = ChooseDirection(body.Position)
				* RandomExt.Range(Consts.AsteroidSpeedRange);
			var radius = ChooseSize(body.Velocity);

			Main.Pub<RequestAsteroid>(new() { PhysicsBody = body, Radius = radius, });
		}

		private Vector2 ChoosePosition()
		{
			var pos = new Vector2(Random.value, Random.value);

			if (Random.Range(0, 2) == 0)
			{
				pos.x = Mathf.Round(pos.x);
			}
			else
			{
				pos.y = Mathf.Round(pos.y);
			}

			return pos;
		}

		private Vector2 ChooseDirection(Vector2 forPosition)
		{
			const int maxTries = 16;

			var towardsCenter = (Vector2.one * .5f - forPosition).normalized;
			Vector2 dir = Vector2.zero;

			// bruteforce randomize
			float dot = -1f;
			int tryCount = 0;
			while (dot < 0.61f && tryCount < maxTries) // 45deg == 0.6 cos
			{
				dir = Random.insideUnitCircle.normalized;
				dot = Vector2.Dot(dir, towardsCenter);
				tryCount++;
			}

			// if failed, fail gracefully
			if (tryCount == maxTries)
				dir = towardsCenter;

			return dir;
		}

		private float ChooseSize(Vector2 velocity)
		{
			var f = MathfExt.InverseLerp(Consts.AsteroidSpeedRange, velocity.magnitude);
			var size = MathfExt.Lerp(Consts.AsteroidSizeRange, f);
			return size;
		}
	}
}