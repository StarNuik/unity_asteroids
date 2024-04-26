using System;
using System.Collections.Generic;

namespace Asteroids
{
	public static class EntityExt
	{
		// i am definitely in need of a `Component`-s (from the ECS) system
		public static bool Is<T>(this Entity entity, List<T> collection)
			where T : IEntity
		{
			return entity.Is<T>(collection, out _);
		}

		public static bool Is<T>(this Entity entity, List<T> collection, out T concrete)
			where T : IEntity
		{
			concrete = default;
			var idx = collection.FindIndex(i => i.Entity == entity);
			if (idx < 0)
				return false;
			
			concrete = collection[idx];
			return true;
		}
	}
}