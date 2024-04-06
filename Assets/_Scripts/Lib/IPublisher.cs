using System;

namespace Asteroids.Lib
{
	public interface IPublisher
	{
		public void Pub<T>(T payload);
	}
}