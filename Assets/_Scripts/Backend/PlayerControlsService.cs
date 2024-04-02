using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Backend
{
	public class PlayerControlsService
	{
		public static void Sub(IEventStream mainStream)
		{
			mainStream.Sub<Tick>(Tick);
		}

		private static void Tick(Tick tick)
		{
			var state = tick.State;
			var input = state.PlayerInput;

			Direction(state, input);
			Acceleration(state, input);
		}

		private static void Direction(SessionState state, InputDelta input)
		{
			if (input.Rotate == 0)
				return;
			
			var dir = state.PlayerDirection;
			var deltaAngle =
				Consts.PlayerAngularSpeed
				* input.Rotate
				* Consts.ServerDeltaTime;
			
			state.PlayerDirection = dir.RotateDegrees(deltaAngle);
		}

		private static void Acceleration(SessionState state, InputDelta input)
		{
			if (!input.Accelerate)
				return;
			
			var vel = state.PlayerVelocity;
			var dir = state.PlayerDirection;
			var add = dir * Consts.PlayerAcceleration * Consts.ServerDeltaTime;

			state.PlayerVelocity = Vector2.ClampMagnitude(vel + add, Consts.PlayerTopSpeed);
		}
	}
}