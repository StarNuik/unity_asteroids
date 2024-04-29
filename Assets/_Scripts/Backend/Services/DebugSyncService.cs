using System;

namespace Asteroids
{
	public class DebugSyncService : Service
	{
		public void PubDebug()
		{
			PhysicsDebug.Record(State);
		}
	}
}