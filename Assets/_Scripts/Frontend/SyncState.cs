using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Frontend
{
	public class SyncState : MonoBehaviour
	{
		private PolledEventStream sock => Locator.StreamIn;
		private SessionState state => Locator.SessionState;

		private void Awake()
		{
			Subscribe();
		}

		private void Update()
		{
			Poll();
		}

		private void Subscribe()
		{
			sock.Sub<PlayerDelta>(ApplyPlayer);
		}

		private void Poll()
		{
			sock.Poll();
		}

		private void ApplyPlayer(PlayerDelta delta)
		{
			delta.ApplyTo(state);
		}
	}
}