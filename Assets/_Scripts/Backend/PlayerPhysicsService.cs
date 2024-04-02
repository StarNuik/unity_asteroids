using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Backend
{
	public class PlayerPhysicsService
	{
		public static void Sub(IEventStream mainStream)
		{
			mainStream.Sub<Tick>(Tick);
		}
		
		private static void Tick(Tick tick)
		{
			var state = tick.State;
			Velocity(state);
			Position(state);

			tick.OutStream.Pub<PlayerDelta>(
				PlayerDelta.ConstructFrom(state)
			);
		}

		private static void Velocity(SessionState state)
		{
			var vel = state.PlayerVelocity;
			var dir = vel.normalized;
			var len = vel.magnitude;

			var next = Mathf.Max(0f, len - Consts.WorldDrag * Consts.ServerDeltaTime);
			state.PlayerVelocity = dir * next;
		}

		private static void Position(SessionState state)
		{
			var vel = state.PlayerVelocity;

			state.PlayerPosition += vel * Consts.ServerDeltaTime;
		}
	}
}