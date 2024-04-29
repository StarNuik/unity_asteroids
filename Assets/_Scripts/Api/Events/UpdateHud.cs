using System;

namespace Asteroids
{
	public struct UpdateHud
	{
		public int Score;

		public UpdateHud(SessionState state)
		{
			Score = state.PlayerScore;
		}
	}
}