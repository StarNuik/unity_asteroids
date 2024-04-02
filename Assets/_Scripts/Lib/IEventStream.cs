using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Lib
{
	public interface IEventStream
	{
		public void Sub<T>(Action<T> listener);
		public void Pub<T>(T payload);
	}
}
