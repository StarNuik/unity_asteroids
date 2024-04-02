using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public interface IStateDelta
	{
		public void ApplyTo(SessionState state);
	}
}