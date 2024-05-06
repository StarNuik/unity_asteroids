using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	public class ObjectsSpawnPropsService : Service
	{
		public void PrepareAsteroid()
		{
			var body = new PhysicsBody();
			body.Position = PointOnBorder();
			body.Velocity = SomewhatTowardsCenter(body.Position)
				* RandomExt.Range(Consts.AsteroidSpeedRange);
			var radius = AsteroidSize(body.Velocity);

			Main.Pub(new RequestAsteroid { PhysicsBody = body, Radius = radius, });
		}

		public void PrepareMissile()
		{
			var body = new PhysicsBody();
			body.Position = PointOnBorder();
			body.Velocity = default;

			Main.Pub(new RequestMissile { PhysicsBody = body, });
		}

		private Vector2 PointOnBorder()
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

		private Vector2 SomewhatTowardsCenter(Vector2 forPosition)
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

		private float AsteroidSize(Vector2 velocity)
		{
			var f = MathfExt.InverseLerp(Consts.AsteroidSpeedRange, velocity.magnitude);
			var size = MathfExt.Lerp(Consts.AsteroidSizeRange, f);
			return size;
		}
	}
}