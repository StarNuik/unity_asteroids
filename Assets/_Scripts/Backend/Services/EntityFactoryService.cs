using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Asteroids.Lib;
using UnityEngine.Assertions;

namespace Asteroids
{
	public class EntityFactoryService : Service
	{
		public Asteroid NewAsteroid()
		{
			var entity = NewEntity();
			var asteroid = new Asteroid()
			{
				PhysicsBody = BulletBody(),
			};

			State.Asteroids.Add(asteroid);
			Client.Pub<CreateAsteroid>(new() { Asteroid = asteroid, });
			return asteroid;
		}

		public Bullet NewBullet()
		{
			var entity = NewEntity();
			var bullet = new Bullet(entity)
			{
				PhysicsBody = BulletBody(),
			};

			State.Bullets.Add(bullet);
			Client.Pub<CreateBullet>(new() { Bullet = bullet, });
			return bullet;
		}

		private Entity NewEntity()
		{
			var entity = new Entity(
				NextId(State)
			);
			
			State.Entities.Add(entity);
			Client.Pub<CreateEntity>(new() { Entity = entity, });

			return entity;
		}

		private PhysicsBody BulletBody()
		{
			return new()
			{
				Position = State.PlayerPosition,
				Velocity = State.PlayerDirection * Consts.PrimaryBulletSpeed,
			};
		}

		private int NextId(SessionState state)
		{
			var id = state.NextId;
			//todo: int overflow
			state.NextId += 1;

			return id;
		}
	}
}