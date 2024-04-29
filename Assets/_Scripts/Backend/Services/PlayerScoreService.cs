using System;
using UnityEngine;

namespace Asteroids
{
	public class PlayerScoreService : Service
	{
		public void TryAddScore(DeleteEntity msg)
		{
			if (msg.Entity.Is(State.Asteroids))
			{
				State.PlayerScore += Consts.AsteroidScore;
				Main.Pub(new UpdateHud(State));
			}
		}
	}
}