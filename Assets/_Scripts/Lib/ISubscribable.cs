using System;

namespace Asteroids
{
	public interface ISubscribable
	{
		public void Sub<T>(Action listener);
		public void Sub<T>(Action<T> listener);
	}
}