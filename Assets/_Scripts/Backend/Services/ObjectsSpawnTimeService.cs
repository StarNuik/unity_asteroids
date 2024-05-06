using System;

namespace Asteroids
{
	public class ObjectsSpawnTimeService : Service
	{
		public void Tick()
		{
			CheckAsteroids();
			CheckMissiles();
		}

		private void CheckAsteroids()
		{
			if (State.Tick < State.NextAsteroid)
				return;
			
			State.NextAsteroid = State.Tick + RandomExt.Range(Consts.AsteroidsTimerRange);
			Main.Pub(new TimeForAsteroid());
		}

		private void CheckMissiles()
		{
			var nextMissile = State.LastMissile + Consts.MissileSpawnScoreDelta;
			if (State.PlayerScore < nextMissile)
				return;
			
			State.LastMissile = State.PlayerScore;
			Main.Pub(new TimeForMissile());
		}
	}
}