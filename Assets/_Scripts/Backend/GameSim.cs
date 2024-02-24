using UnityEngine;

namespace Asteroids
{
	public class GameSim
	{
		public GameState State { get; private set; } = new();

		public InputMap PlayerInput { private get; set; }

		public void Tick()
		{
			PlayerDirection();
			PlayerVelocity();
			PlayerPosition();

			State.Tick++;
		}

		private void PlayerDirection()
		{
			if (PlayerInput.Rotate == 0)
				return;
			
			State.PlayerDirection = State.PlayerDirection.Rotate(
				Consts.PlayerAngularSpeed
				* PlayerInput.Rotate
				* Consts.ServerDeltaTime
			);
		}

		private void PlayerVelocity()
		{
			var vel = State.PlayerVelocity;
			var dir = State.PlayerDirection;
			var len = vel.magnitude;
			var next = 0f;
			
			if (PlayerInput.Accelerate)
			{
				next = Mathf.Min(Consts.PlayerTopSpeed, len + Consts.PlayerAcceleration * Consts.ServerDeltaTime);
			}
			else
			{
				next = Mathf.Min(0f, len - Consts.WorldDrag * Consts.ServerDeltaTime);
			}
			State.PlayerVelocity = dir * next;
		}

		private void PlayerPosition()
		{
			State.PlayerPosition += State.PlayerVelocity * Consts.ServerDeltaTime;
		}
	}
}