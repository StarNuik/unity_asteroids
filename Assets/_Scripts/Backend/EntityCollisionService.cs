using System;
using System.Collections.Generic;

namespace Asteroids
{
	public abstract class EntityCollisionService<TEntity> : Service
		where TEntity : IColliderEntity
	{
		private Action<Dictionary<Entity, TEntity>, TEntity> Destructor;
		
		protected abstract Dictionary<Entity, TEntity> Entities { get; }

		public void Inject(Action<Dictionary<Entity, TEntity>, TEntity> destructor)
		{
			Destructor = destructor;
		}

		public void TryCollision(Collision msg)
		{
			if (msg.Entity.Is<TEntity>(Entities, out var concrete))
			{
				ProcessCollision(concrete, msg.Other);
			}
		}

		protected void Destroy(TEntity concrete)
		{
			Destructor(Entities, concrete);
		}

		protected abstract void ProcessCollision(TEntity who, Entity other);
	}
}