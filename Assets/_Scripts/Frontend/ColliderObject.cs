using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class ColliderObject : MonoBehaviour
	{
		protected FieldService Field => Locator.Field;

		private Vector3 awakeScale;

		public float Radius
		{
			set => transform.localScale = awakeScale * Field.TransformDimension(value);
		}

		public Vector2 Position
		{
			set => transform.position = Field.ToWorld(value.ToXY0());
		}

		private void Awake()
		{
			awakeScale = transform.localScale;
		}
	}
}
