using System.Collections.Generic;
using Asteroids.Frontend;
using UnityEngine;
using UnityEngine.Assertions;

namespace Asteroids
{
	public class EntitiesHelperService : MonoBehaviour
	{
		// is this bad? or, rather,
		// how can i make this better, w/o too much effort
		private FieldService field => Locator.Field;

		public void NewEntity<TPrefab, TData>(Dictionary<Entity, TPrefab> collection, TData info, TPrefab prefab)
			where TPrefab : MonoBehaviour, IClonedPhysicsBody
			where TData : IPhysicsEntity
		{
			Assert.IsNotNull(field);

			var instance = Object.Instantiate(prefab);
			instance.Position = field.ToWorld(info.PhysicsBody.Position);

			collection.Add(info.Entity, instance);
		}

		public void TryUpdateEntity<TPrefab>(Dictionary<Entity, TPrefab> collection, UpdatePhysicsEntity update)
			where TPrefab : MonoBehaviour, IClonedPhysicsBody
		{
			var entity = update.Entity;
			if (!collection.ContainsKey(entity))
				return;
			
			var item = collection[entity];
			item.Position = field.ToWorld(update.PhysicsBody.Position);
		}

		public void TryDeleteEntity<TPrefab>(Dictionary<Entity, TPrefab> collection, DeleteEntity msg)
			where TPrefab : MonoBehaviour, IClonedPhysicsBody
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