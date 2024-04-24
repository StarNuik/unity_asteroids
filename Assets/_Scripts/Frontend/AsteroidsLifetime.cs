using System;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class AsteroidsLifetime : MonoBehaviour
	{
		private ISubscribable server => Locator.ServerIn;
		// private FieldService field => Locator.Field;

		private void Awake()
		{
			server.Sub<CreateAsteroid>(RecreateAsteroid);
		}

		private void RecreateAsteroid(CreateAsteroid asteroid)
		{
			//
			Debug.Log("[ AsteroidsLifetime.RecreateAsteroid ]");
		}
	}
}
