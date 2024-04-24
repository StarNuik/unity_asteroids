using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public interface IPhysicsBody
	{
		public PhysicsBody PhysicsBody { get; set; }
	}
}