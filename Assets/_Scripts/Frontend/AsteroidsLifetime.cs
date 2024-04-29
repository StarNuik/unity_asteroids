using System;
using System.Collections.Generic;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class AsteroidsLifetime : MonoBehaviour
	{
		[Editor] AsteroidObject asteroidPrefab;

		private ISubscribable Server => Locator.ServerIn;
		private EntitiesHelperService EntitiesHelper => Locator.EntitiesHelper;

		private Dictionary<Entity, AsteroidObject> asteroids = new();

		private void Awake()
		{
			Server.Sub<CreateAsteroid>(NewAsteroid);
			Server.Sub<UpdatePhysicsEntity>(TryUpdateAsteroid);
			Server.Sub<DeleteEntity>(TryDeleteAsteroid);
		}

		private void NewAsteroid(CreateAsteroid msg)
			=> EntitiesHelper.NewEntity(asteroids, msg.Asteroid, asteroidPrefab);

		private void TryUpdateAsteroid(UpdatePhysicsEntity update)
			=> EntitiesHelper.TryUpdatePhysics(asteroids, update);

		private void TryDeleteAsteroid(DeleteEntity msg)
			=> EntitiesHelper.TryDeleteEntity(asteroids, msg);
	}
}
