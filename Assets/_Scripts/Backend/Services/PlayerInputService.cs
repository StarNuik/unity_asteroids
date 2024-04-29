using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids
{
	public class PlayerInputService : Service
	{
		public void ApplyInput(UpdateInput update)
		{
			State.PlayerInput = update;
		}
	}
}