using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public struct PlayerDelta : IStateDelta
	{
		public Vector2 Position;
		public Vector2 Direction;

		public void ApplyTo(SessionState state)
		{
			state.PlayerPosition = Position;
			state.PlayerDirection = Direction;
		}

		public static PlayerDelta ConstructFrom(SessionState from)
		{
			var delta = new PlayerDelta()
			{
				Position = from.PlayerPosition,
				Direction = from.PlayerDirection,
			};
			return delta;
		}
	}
}