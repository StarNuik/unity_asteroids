using System.Collections.Generic;
using Asteroids.Frontend;
using UnityEngine;
using UnityEngine.Assertions;

namespace Asteroids
{
	public class EntitiesHelperService : MonoBehaviour
	{
		public void NewEntity<TPrefab, TData>(Dictionary<Entity, TPrefab> collection, TData info, TPrefab prefab)
			where TPrefab : ColliderObject
			where TData : IColliderEntity
		{
			var instance = Object.Instantiate(prefab);
			instance.Position = info.PhysicsBody.Position;
			instance.Radius = info.Radius;

			collection.Add(info.Entity, instance);
		}

		public void TryUpdateEntity<TPrefab>(Dictionary<Entity, TPrefab> collection, UpdatePhysicsEntity update)
			where TPrefab : ColliderObject
		{
			var entity = update.Entity;
			if (!collection.ContainsKey(entity))
				return;
			
			var item = collection[entity];
			item.Position = update.PhysicsBody.Position;
		}

		public void TryDeleteEntity<TPrefab>(Dictionary<Entity, TPrefab> collection, DeleteEntity msg)
			where TPrefab : ColliderObject
		{
			var entity = msg.Entity;
			if (!collection.ContainsKey(entity))
				return;
			
			collection.Remove(entity, out var item);
			Object.Destroy(item.gameObject);
		}

		private void Awake()
		{
			Locator.EntitiesHelper = this;
		}
	}
}