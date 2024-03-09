using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Backend
{
	public class PlayerInputService
	{
		// should I store this in the GameState as well?
		private InputMap currentInput;

		public void Subscribe(IEventSocket sock)
		{
			sock.Subscribe<InputMap>(SaveInput);
		}

		public void Tick(ref GameState state)
		{
			Direction(ref state);
			Acceleration(ref state);
		}

		private void SaveInput(InputMap nextInput)
		{
			currentInput = nextInput;
		}

		private void Direction(ref GameState state)
		{
			if (currentInput.Rotate == 0)
				return;
			
			var dir = state.PlayerDirection;
			var deltaAngle =
				Consts.PlayerAngularSpeed
				* currentInput.Rotate
				* Consts.ServerDeltaTime;
			
			state.PlayerDirection = dir.RotateDegrees(deltaAngle);
		}

		private void Acceleration(ref GameState state)
		{
			if (!currentInput.Accelerate)
				return;
			
			var vel = state.PlayerVelocity;
			var dir = state.PlayerDirection;
			var add = dir * Consts.PlayerAcceleration * Consts.ServerDeltaTime;

			state.PlayerVelocity = Vector2.ClampMagnitude(vel + add, Consts.PlayerTopSpeed);
		}
	}
}