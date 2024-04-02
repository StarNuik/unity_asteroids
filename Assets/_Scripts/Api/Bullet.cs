using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public struct Bullet : IPhysicsEntity
	{
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
	}
}
