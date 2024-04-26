using System;
using UnityEngine;

namespace Asteroids
{
	public interface IColliderEntity : IPhysicsEntity
	{
		public float Radius { get; }
	}
}