using System.Collections.Generic;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class PhysicsDebug : MonoBehaviour
	{
		private static List<RecordedCollider> records = new();
		private FieldService Field => Locator.Field;

		public static void Record(SessionState state)
		{
			records.Clear();
			foreach (var collider in state.ColliderEntities)
			{
				records.Add(new(collider));
			}
		}

		private void OnDrawGizmos()
		{
			if (Field == null)
				return;
			
			foreach (var r in records)
			{
				r.DrawGizmos(Field);
			}
		}

		private record RecordedCollider
		{
			public float Radius;
			public PhysicsBody PhysicsBody;

			public RecordedCollider(IColliderEntity collider)
			{
				Radius = collider.Radius;
				PhysicsBody = collider.PhysicsBody;
			}

			public void DrawGizmos(FieldService field)
			{
				Gizmos.color = Color.yellow;

				var pos = field.ToWorld(PhysicsBody.Position);
				var vel = field.TransformVector(PhysicsBody.Velocity);
				var size = field.TransformDimension(Radius);

				Gizmos.DrawWireSphere(pos, size);
				// Gizmos.DrawLine(pos, pos + vel);
			}
		}
	}
}
