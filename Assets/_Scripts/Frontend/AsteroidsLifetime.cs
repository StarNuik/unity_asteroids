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
			server.Sub<CreatedAsteroid>(RecreateAsteroid);
		}

		private void RecreateAsteroid(CreatedAsteroid asteroid)
		{
			//
			Debug.Log("[ AsteroidsLifetime.RecreateAsteroid ]");
		}
	}
}
