using System;
using System.Collections.Generic;
using Asteroids.Lib;

namespace Asteroids
{
	public class EntityRegistryService : Service
	{
		public void RegisterEntity(EntityCreated created)
		{
			var bullets = new List<Bullet>();
			var asteroids = new List<Asteroid>();
			var pEntities = new List<IPhysicsEntity>();

			// this is boxing galore
			//todo: switch to `record`-s
			foreach (var entity in State.Entities)
			{
				if (entity is Bullet)
				{
					bullets.Add((Bullet)entity);
				}
				if (entity is Asteroid)
				{
					asteroids.Add((Asteroid)entity);
				}
				if (entity is IPhysicsEntity)
				{
					pEntities.Add((IPhysicsEntity)entity);
				}
			}

			State.Bullets = bullets.AsReadOnly();
			State.Asteroids = asteroids.AsReadOnly();
			State.PhysicsEntities = pEntities.AsReadOnly();
		}
	}
}