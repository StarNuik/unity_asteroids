using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class DeltaService : MonoBehaviour
	{
		public void Subscribe(IEventSocket sock)
		{
			sock.Subscribe<InputDelta>(ApplyInput);
		}

		public void Poll(IEventSocket sock)
		{}

		public void Send(IEventSocket sock)
		{}

		private void ApplyInput(InputDelta delta)
		{
			
		}
	}
}