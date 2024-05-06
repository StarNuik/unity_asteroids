using System;
using UnityEngine;

namespace Asteroids
{
	public class PlayerScoreService : Service
	{
		public void TryAddScore(DeleteEntity msg)
		{
			var scoreDelta = 0;
			if (msg.Entity.Is(State.Asteroids))
			{
				scoreDelta += Consts.AsteroidScore;
			}

			if (msg.Entity.Is(State.Missiles))
			{
				scoreDelta += Consts.MissileScore;
			}

			if (scoreDelta == 0)
				return;
			
			State.PlayerScore += scoreDelta;
			Main.Pub(new UpdateHud(State));
		}
	}
}