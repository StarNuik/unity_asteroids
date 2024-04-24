using System;

namespace Asteroids
{
	// hopefully, a temporary solution
	public class SyncService : Service
	{
		public void PubUpdates()
		{
			Client.Pub(
				PlayerDelta.ConstructFrom(State)
			);
						
			foreach (var entity in State.PhysicsEntities)
			{
				Client.Pub<UpdatePhysicsEntity>(new(entity));
			}
		}
	}
}