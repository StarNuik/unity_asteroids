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
		// `stateList` is yucky,
		// but I don't want to spend time
		// properly implementing components from ECS
		public void Delete<T>(List<T> stateList, T item)
			where T : IEntity
		{
			var idx = stateList.FindIndex(i => i.Entity == item.Entity);
			Assert.IsTrue(idx >= 0);
			stateList.RemoveAt(idx);

			DeleteEntity(item.Entity);
		}

		public Asteroid NewAsteroid()
		{
			var entity = NewEntity();
			var asteroid = new Asteroid(entity)
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
			var bullet = new Bullet(entity, State.Tick)
			{
				PhysicsBody = BulletBody(),
			};

			State.Bullets.Add(bullet);
			Client.Pub<CreateBullet>(new() { Bullet = bullet, });
			return bullet;
		}

		private void Clean<T>(List<T> items, IEntity entity)
			where T : IEntity
		{
			var idx = items.FindIndex(i => i.Entity == entity.Entity);
			items.RemoveAt(idx);
		}

		private void DeleteEntity(Entity entity)
		{
			var idx = State.Entities.FindIndex(e => e == entity);
			State.Entities.RemoveAt(idx);
			Client.Pub<DeleteEntity>(new() { Entity = entity, });
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