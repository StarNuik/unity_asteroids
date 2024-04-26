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
		public void FinishDeletes()
		{
			foreach (var delete in State.QueuedDeletes)
			{
				delete();
			}
			State.QueuedDeletes.Clear();
		}

		public void QueueDelete<T>(List<T> stateList, T item)
			where T : IEntity
		{
			State.QueuedDeletes.Add(() => {
				if (!State.Entities.Contains(item.Entity))
					return;
				Delete(stateList, item);
			});
		}

		// `stateList` is yucky,
		// but I don't want to spend time
		// properly implementing components from ECS
		private void Delete<T>(List<T> stateList, T item)
			where T : IEntity
		{
			var idx = stateList.FindIndex(i => i.Entity == item.Entity);
			Assert.IsTrue(idx >= 0);
			stateList.RemoveAt(idx);

			DeleteEntity(item.Entity);
		}

		public void NewAsteroid(RequestAsteroid req)
		{
			var entity = NewEntity();
			var asteroid = new Asteroid(entity, req.Radius)
			{
				PhysicsBody = req.PhysicsBody,
			};

			State.Asteroids.Add(asteroid);
			Client.Pub<CreateAsteroid>(new() { Asteroid = asteroid, });
			// return asteroid;
		}

		public void NewBullet(RequestBullet req)
		{
			var entity = NewEntity();
			var bullet = new Bullet(entity, State.Tick, Consts.BulletRadius)
			{
				PhysicsBody = req.PhysicsBody,
			};

			State.Bullets.Add(bullet);
			Client.Pub<CreateBullet>(new() { Bullet = bullet, });
			// return bullet;
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

		private int NextId(SessionState state)
		{
			var id = state.NextId;
			//todo: int overflow
			state.NextId += 1;

			return id;
		}
	}
}