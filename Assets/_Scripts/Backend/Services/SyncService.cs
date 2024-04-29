using System;
using Asteroids.Lib;

namespace Asteroids
{
	// hopefully, a temporary solution
	public class SyncService : Service
	{
		private IPublisher Client;

		public void Inject(IPublisher client)
		{
			Client = client;
		}

		public void PubUpdates()
		{
			foreach (var entity in State.PhysicsEntities)
			{
				Client.Pub(new UpdatePhysicsEntity(entity));
			}

			foreach (var (_, actor) in State.Actors)
			{
				Client.Pub(new UpdateActor(actor));
			}
		}

		public void Rebroadcast<T>(T msg)
		{
			Client.Pub(msg);
		}
	}
}