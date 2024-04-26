using System;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class BulletObject : MonoBehaviour, IClonedPhysicsBody
	{
		public Vector3 Position
		{
			set => transform.position = value;
		}

		public Vector3 Velocity { get; set; }

		private void Update()
		{
			// transform.position += Velocity * Time.deltaTime;
		}
	}
}
