using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class AsteroidObject : MonoBehaviour, IClonedPhysicsBody
	{
		public Vector3 Position {
			set => transform.position = value;
		}
	}
}
