using UnityEngine;

namespace Asteroids
{
	public class SessionState
	{
		public int Tick;

		public InputDelta PlayerInput;
		
		public Vector2 PlayerPosition;
		public Vector2 PlayerVelocity;
		public Vector2 PlayerDirection;

		public SessionState(int _ = 0)
		{
			PlayerInput = new();

			PlayerPosition = Vector2.one * 0.5f;
			PlayerVelocity = Vector2.zero;
			PlayerDirection = Vector2.right;
			
			Tick = 0;
		}
	}
}