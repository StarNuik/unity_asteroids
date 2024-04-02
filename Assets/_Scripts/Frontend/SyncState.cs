using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Lib;
using UnityEngine;

namespace Asteroids.Frontend
{
	public class SyncState : MonoBehaviour
	{
		private IEventSocket sock => Locator.Socket;
		private SessionState state => Locator.SessionState;

		private void Awake()
		{
			Subscribe();
		}

		private void Update()
		{
			Poll();
		}

		private void LateUpdate()
		{
			Send();
		}

		private void Subscribe()
		{
			sock.Subscribe<PlayerDelta>(ApplyPlayer);
		}

		private void Poll()
		{
			sock.Poll<PlayerDelta>();
		}

		private void Send()
		{
			sock.Send<InputDelta>(InputDelta.ConstructFrom(state));
		}

		private void ApplyPlayer(PlayerDelta delta)
		{
			delta.ApplyTo(state);
		}
	}
}