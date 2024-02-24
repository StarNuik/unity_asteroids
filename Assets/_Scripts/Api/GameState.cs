using UnityEngine;

namespace Asteroids
{
	public class GameState
	{
		public int Tick;
		
		public Vector2 PlayerPosition = Vector2.one * 0.5f;
		public Vector2 PlayerVelocity = Vector2.zero;
		public Vector2 PlayerDirection = Vector2.up;
	}
}