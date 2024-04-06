using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class Installer : MonoBehaviour
	{
		private void Awake()
		{
			Locator.Field = FindObjectOfType<FieldService>();
		}
	}
}
