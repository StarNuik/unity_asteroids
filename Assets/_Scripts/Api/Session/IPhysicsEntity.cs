using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public interface IPhysicsEntity : IEntity
	{
		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
	}
}