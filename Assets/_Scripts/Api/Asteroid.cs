using System;
using UnityEngine;

namespace Asteroids
{
	public struct Asteroid : IPhysicsEntity
	{
		public int Id { get; set; }
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
	}
}