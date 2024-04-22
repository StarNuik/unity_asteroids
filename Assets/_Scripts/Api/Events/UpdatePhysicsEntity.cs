using System;
using UnityEngine;

namespace Asteroids
{
	public struct UpdatePhysicsEntity
	{
		public int Id;
		public Vector2 Position;
		public Vector2 Velocity;

		public static UpdatePhysicsEntity From(IPhysicsEntity body)
		{
			return new()
			{
				Id = body.Id,
				Position = body.Position,
				Velocity = body.Velocity,
			};
		}
	}
}