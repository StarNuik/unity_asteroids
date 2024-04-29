using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class ActorObject : ColliderObject
	{
		public Vector2 Direction
		{
			set => transform.rotation = Quaternion.LookRotation(value.ToXY0(), Vector3.forward);
		}
	}
}
