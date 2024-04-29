using Asteroids.App;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	[DefaultExecutionOrder(-998)]
	public class UpdateConnection : MonoBehaviour
	{
		private ServerConnection Connection => Locator.Connection;

		private void Start()
		{
			Connection.IsEnabled = true;
		}

		private void Update()
		{
			Connection.Poll();
		}
	}
}
