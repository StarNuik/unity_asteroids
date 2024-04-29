using Asteroids.App;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	[DefaultExecutionOrder(-999)]
	public class Installer : MonoBehaviour
	{
		private void Awake()
		{
			Locator.Field = FindObjectOfType<FieldService>();
			Locator.EntitiesHelper = FindObjectOfType<EntitiesHelperService>();
			Locator.Connection = FindObjectOfType<ServerConnection>();
		}
	}
}
