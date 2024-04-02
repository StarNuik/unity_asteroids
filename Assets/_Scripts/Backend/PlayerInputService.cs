using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Backend
{
	public class PlayerInputService
	{
		public void Tick(SessionState state)
		{
			var input = state.PlayerInput;

			Direction(state, input);
			Acceleration(state, input);
		}

		private void Direction(SessionState state, InputDelta input)
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

		private void Acceleration(SessionState state, InputDelta input)
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