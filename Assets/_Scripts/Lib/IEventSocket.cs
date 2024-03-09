using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Lib
{
	public interface IEventSocket
	{
		public void Subscribe<T>(Action<T> listener)
			where T : struct;
			
		public void Send<T>(T payload)
			where T : struct;

		public void Poll();
	}
}
