using System;
using UnityEngine;

namespace Asteroids
{
	public interface IPhysicsCollider // : IPhysicsEntity
	{
		public Vector2 Position { get; }
		public float Radius { get; }
	}
}