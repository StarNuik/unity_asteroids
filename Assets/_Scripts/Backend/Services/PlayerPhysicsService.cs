using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Backend
{
	public class PlayerPhysicsService : Service
	{
		public void Tick(Tick tick)
		{
			Velocity();
			Position();
		}

		private void Velocity()
		{
			var vel = State.PlayerVelocity;
			var dir = vel.normalized;
			var len = vel.magnitude;

			var next = Mathf.Max(0f, len - Consts.WorldDrag * Consts.ServerDeltaTime);
			State.PlayerVelocity = dir * next;
		}

		private void Position()
		{
			var vel = State.PlayerVelocity;

			State.PlayerPosition += vel * Consts.ServerDeltaTime;
		}
	}
}