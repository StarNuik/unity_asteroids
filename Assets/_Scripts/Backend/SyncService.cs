using System;

namespace Asteroids
{
	// hopefully, a temporary solution
	public class SyncService : Service
	{
		public void PubUpdates(Sync _)
		{
			// Client.Pub(
			// 	PlayerDelta.ConstructFrom(State)
			// );

			if (State.PhysicsEntities == null)
				return;
			
			foreach (var body in State.PhysicsEntities)
			{
				Client.Pub(
					UpdatePhysicsEntity.From(body)
				);
			}
		}
	}
}