using System;
using System.Collections.Generic;

namespace Asteroids
{
	public static class EntityExt
	{
		// i am definitely in need of a `Component`-s (from the ECS) system
		public static bool Is<T>(this Entity entity, Dictionary<Entity, T> collection)
			where T : IEntity
		{
			return collection.ContainsKey(entity);
		}

		public static bool Is<T>(this Entity entity, Dictionary<Entity, T> collection, out T concrete)
			where T : IEntity
		{
			concrete = collection.GetValueOrDefault(entity);
			return collection.ContainsKey(entity);
		}
	}
}