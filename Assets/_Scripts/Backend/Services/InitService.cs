using System;

namespace Asteroids
{
	public class InitService : Service
	{
		public void Init()
		{
			Main.Pub(new RequestActor());
		}
	}
}