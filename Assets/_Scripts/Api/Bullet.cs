using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public struct Bullet : IPhysicsEntity
	{
		public int Id { get; set; }
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
	}
}
