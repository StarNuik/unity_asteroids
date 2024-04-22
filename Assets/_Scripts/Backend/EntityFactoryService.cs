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
		public T NewEntity<T>()
			where T : struct, IEntity
		{
			var entity = default(T);
			entity.Id = GetId(State);
			State.Entities.Add(entity);
			Main.Pub(new EntityCreated());

			return entity;
		}

		public static int GetId(SessionState state)
		{
			var id = state.NextId;
			//todo: int overflow
			state.NextId += 1;

			return id;
		}
	}
}