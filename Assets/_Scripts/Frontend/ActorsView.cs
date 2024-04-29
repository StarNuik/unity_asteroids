using System.Collections.Generic;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class ActorsView : MonoBehaviour
	{
		[Editor] ActorObject actorPrefab;

		private ISubscribable Server => Locator.ServerIn;
		private EntitiesHelperService EntitiesHelper => Locator.EntitiesHelper;

		private Dictionary<Entity, ActorObject> actors = new();

		private void Awake()
		{
			Server.Sub<CreateActor>(NewActor);
			Server.Sub<UpdatePhysicsEntity>(TryUpdatePhysics);
			Server.Sub<UpdateActor>(TryUpdateActor);
			Server.Sub<DeleteEntity>(TryDeleteActor);
		}

		private void NewActor(CreateActor msg)
			=> EntitiesHelper.NewEntity(actors, msg.Actor, actorPrefab);

		private void TryUpdatePhysics(UpdatePhysicsEntity update)
			=> EntitiesHelper.TryUpdateEntity(actors, update);

		private void TryDeleteActor(DeleteEntity msg)
			=> EntitiesHelper.TryDeleteEntity(actors, msg);

		private void TryUpdateActor(UpdateActor update)
		{
			var entity = update.Entity;
			if (!actors.ContainsKey(entity))
				return;
			
			var actor = actors[entity];
			actor.Direction = update.Direction;
		}
	}
}
