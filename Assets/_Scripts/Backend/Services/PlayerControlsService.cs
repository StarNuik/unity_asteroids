using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Backend
{
	public class PlayerControlsService : Service
	{
		public void Tick(Tick tick)
		{
			var input = State.PlayerInput;

			Direction(input);
			Acceleration(input);
		}

		private void Direction(InputDelta input)
		{
			if (input.Rotate == 0)
				return;
			
			var dir = State.PlayerDirection;
			var deltaAngle =
				Consts.PlayerAngularSpeed
				* input.Rotate
				* Consts.ServerDeltaTime;
			
			State.PlayerDirection = dir.RotateDegrees(deltaAngle);
		}

		private void Acceleration(InputDelta input)
		{
			if (!input.Accelerate)
				return;
			
			var vel = State.PlayerVelocity;
			var dir = State.PlayerDirection;
			var add = dir * Consts.PlayerAcceleration * Consts.ServerDeltaTime;

			State.PlayerVelocity = Vector2.ClampMagnitude(vel + add, Consts.PlayerTopSpeed);
		}
	}
}