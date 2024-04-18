using System;
using Asteroids.Lib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	public class AsteroidFactoryService
	{
		public static void Sub(IEventStream main)
		{
			main.Sub<Tick>(Tick);
		}

		private static void Tick(Tick tick)
		{
			var state = tick.State;
			var client = tick.ClientStream;

			if (state.Tick < state.NextAsteroid)
				return;
			
			state.NextAsteroid = state.Tick + Random.Range(Consts.AsteroidsTimerRange.x, Consts.AsteroidsTimerRange.y);

			var a = new Asteroid
			{
				Position = state.PlayerPosition,
				Velocity = state.PlayerDirection * Consts.PrimaryBulletSpeed,
			};

			state.Asteroids.Add(a);
			client.Pub<CreatedAsteroid>(new() { Asteroid = a, });
		}
	}
}