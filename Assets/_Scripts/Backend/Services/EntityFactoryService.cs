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

			//WARNING: the event is sent out BEFORE the entity is removed from the Set<>. MAY cause side effects
			// this event fire location is highly dubious
			Main.Pub(new DeleteEntity() { Entity = item.Entity, });

			stateCollection.Remove(item.Entity);
			DeleteEntity(item.Entity);
		}

		public void NewMissile(RequestMissile request) =>
			NewConcrete(
				(e, req) => new Missile(e, req),
				missile => new CreateMissile { Missile = missile, },
				State.Missiles,
				request
			);

		public void NewActor(RequestActor request) =>
			NewConcrete(
				(e, req) => new PlayerActor(e, req),
				actor => new CreateActor() { Actor = actor, },
				State.Actors,
				request
			);

		public void NewAsteroid(RequestAsteroid request) =>
			NewConcrete(
				(e, req)  => new Asteroid(e, req),
				asteroid => new CreateAsteroid() { Asteroid = asteroid, },
				State.Asteroids,
				request
			);

		public void NewBullet(RequestBullet request) =>
			NewConcrete(
				(e, req) => new Bullet(e, req),
				bullet => new CreateBullet() { Bullet = bullet, },
				State.Bullets,
				request
			);

		// this is yucky, but so are deadlines
		private void NewConcrete<TEntity, TRequest, TMessage>(
			Func<Entity, TRequest, TEntity> newItem,
			Func<TEntity, TMessage> newMessage,
			Dictionary<Entity, TEntity> collection,
			TRequest request
		)
		{
			var entity = NewEntity();
			var item = newItem(entity, request);

			collection.Add(entity, item);
			Main.Pub(newMessage(item));
		}

		private void DeleteEntity(Entity entity)
		{
			State.Entities.Remove(entity);
		}

		private Entity NewEntity()
		{
			var candidate = new Entity(NextId());
			while (!State.Entities.Add(candidate))
				candidate = new Entity(NextId());
			
			var entity = candidate;
			Main.Pub(new CreateEntity() { Entity = entity, });

			return entity;
		}

		private int NextId()
		{
			var id = unchecked(State.LastId + 1);
			State.LastId = id;
			return id;
		}
	}
}