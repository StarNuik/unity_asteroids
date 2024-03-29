using UnityEngine;

namespace Asteroids
{
	public struct GameState
	{
		public int Tick;
		
		public Vector2 PlayerPosition;
		public Vector2 PlayerVelocity;
		public Vector2 PlayerDirection;

		public GameState(int _ = 0)
		{
			PlayerPosition = Vector2.one * 0.5f;
			PlayerVelocity = Vector2.zero;
			PlayerDirection = Vector2.right;
			
			Tick = 0;
		}
	}
}