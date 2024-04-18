using System;
using UnityEngine;

namespace Asteroids
{
	public interface IPhysicsCollider
	{
		public Vector2 Position { get; }
		public float Radius { get; }
	}
}