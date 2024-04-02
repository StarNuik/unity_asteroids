using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class DeltaService
	{
		private SessionState state;

		public DeltaService(SessionState state)
		{
			this.state = state;
		}

		public void Sub(IEventStream inStream)
		{
			inStream.Sub<InputDelta>(ApplyInput);
		}

		public void ApplyInput(InputDelta delta)
		{
			delta.ApplyTo(state);
		}
	}
}