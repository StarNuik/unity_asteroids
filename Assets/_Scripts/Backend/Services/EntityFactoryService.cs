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

		public void QueueDelete<T>(Dictionary<Entity, T> stateCollection, T item)
			where T : IEntity
		{
			State.QueuedDeletes.Add(() => {
				if (!State.Entities.Contains(item.Entity))
					return;
				Delete(stateCollection, item);
			});
		}

		// `Dict<,T>` is yucky,
		// but I don't want to spend time properly implementing the "Component" from "ECS"
		private void Delete<T>(Dictionary<Entity, T> stateCollection, T item)
			where T : IEntity
		{
			Assert.IsTrue(stateCollection.ContainsKey(item.Entity));
			stateCollection.Remove(item.Entity);

			DeleteEntity(item.Entity);
		}

		public void NewAsteroid(RequestAsteroid req)
		{
			var entity = NewEntity();
			var asteroid = new Asteroid(entity, req.Radius)
			{
				PhysicsBody = req.PhysicsBody,
			};

			State.Asteroids.Add(entity, asteroid);
			Client.Pub<CreateAsteroid>(new() { Asteroid = asteroid, });
		}

		public void NewBullet(RequestBullet req)
		{
			var entity = NewEntity();
			var bullet = new Bullet(entity, State.Tick, Consts.BulletRadius)
			{
				PhysicsBody = req.PhysicsBody,
			};

			State.Bullets.Add(entity, bullet);
			Client.Pub<CreateBullet>(new() { Bullet = bullet, });
		}

		private void DeleteEntity(Entity entity)
		{
			State.Entities.Remove(entity);
			Client.Pub<DeleteEntity>(new() { Entity = entity, });
		}

		private Entity NewEntity()
		{
			var candidate = new Entity(NextId());
			while (!State.Entities.Add(candidate))
				candidate = new Entity(NextId());
			
			var entity = candidate;
			Client.Pub<CreateEntity>(new() { Entity = entity, });

			return entity;
		}

		private int NextId()
		{
			var id = State.NextId;
			State.NextId = unchecked(State.NextId + 1);
			return id;
		}
	}
}