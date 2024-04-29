using System;
using UnityEngine;

namespace Asteroids
{
	public struct UpdateActor
	{
		public Entity Entity;
		public Vector2 Direction;

		public UpdateActor(PlayerActor actor)
		{
			Entity = actor.Entity;
			Direction = actor.Direction;
		}
	}
}