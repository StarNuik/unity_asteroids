using UnityEngine;

namespace Asteroids.Backend
{
	public class PlayerPhysicsService
	{
		public void Tick(SessionState state)
		{
			Velocity(state);
			Position(state);
		}

		private void Velocity(SessionState state)
		{
			var vel = state.PlayerVelocity;
			var dir = vel.normalized;
			var len = vel.magnitude;

			var next = Mathf.Max(0f, len - Consts.WorldDrag * Consts.ServerDeltaTime);
			state.PlayerVelocity = dir * next;
		}

		private void Position(SessionState state)
		{
			var vel = state.PlayerVelocity;

			state.PlayerPosition += vel * Consts.ServerDeltaTime;
		}
	}
}