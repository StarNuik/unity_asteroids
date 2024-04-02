using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Frontend
{
	public class SyncState : MonoBehaviour
	{
		private void Awake()
		{
			Locator.Socket.Subscribe<SessionState>(SaveState);
		}

		private void SaveState(SessionState next)
		{
			Locator.GameState = next;
			// Debug.Log($"[ SyncState.SaveState() ] Receieved new state, pp: {next.PlayerPosition}, pv: {next.PlayerVelocity}, pd: {next.PlayerDirection}");
		}

		private void Update()
		{
			Locator.Socket.Poll<SessionState>();
		}
	}
}