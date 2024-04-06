using System;

namespace Asteroids
{
	public struct CreateBullet
	{
		//todo this is *not right*
		public Tick Tick;

		public static CreateBullet From(Tick tick)
		{
			return new() { Tick = tick };
		}
	}
}