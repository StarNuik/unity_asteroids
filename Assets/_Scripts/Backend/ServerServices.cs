using System;
using System.Collections.Generic;
using Asteroids.Backend;

namespace Asteroids
{
	public class ServerServices : Service
	{
		private DeltaService deltaService = new();
		private EntityFactoryService entityFactory = new();
		private AsteroidTimedSpawnService asteroidTimedSpawn = new();
		private SyncService sync = new();
		private ActorsMovementService actorsControls = new();
		private ActorsAttackService actorsAttack = new();
		private PhysicsService physics = new();
		private BulletsTimeoutService bulletsTimeout = new();
		private CollisionsService collisions = new();
		private BulletsCollisionsService bulletsCollisions = new();
		private AsteroidsCollisionsService asteroidsCollisions = new();
		private InitService init = new();

		private DebugSyncService debugSync = new();

		public void Setup()
		{
			InjectChildren();

			Main.Sub<Init>(init.Init);

			Input.Sub<InputDelta>(deltaService.ApplyInput);

			Main.Sub<Tick>(physics.Tick);
			Main.Sub<Tick>(actorsControls.Tick);
			Main.Sub<Tick>(actorsAttack.Tick);
			Main.Sub<Tick>(asteroidTimedSpawn.Tick);
			Main.Sub<Tick>(bulletsTimeout.Tick);
			Main.Sub<Tick>(collisions.Tick);

			// subtick
			Main.Sub<RequestActor>(entityFactory.NewActor);
			Main.Sub<RequestAsteroid>(entityFactory.NewAsteroid);
			Main.Sub<RequestBullet>(entityFactory.NewBullet);
			Main.Sub<Collision>(bulletsCollisions.TryCollision);
			Main.Sub<Collision>(asteroidsCollisions.TryCollision);

			Main.Sub<FinishQueued>(entityFactory.FinishDeletes);

			Main.Sub<Sync>(debugSync.PubDebug);
			Main.Sub<Sync>(sync.PubUpdates);
		}

		private void InjectChildren()
		{
			var services = new List<Service>() { init, debugSync, asteroidsCollisions, bulletsCollisions, collisions, bulletsTimeout, physics, actorsAttack, actorsControls, sync, deltaService, entityFactory, asteroidTimedSpawn, };

			services.ForEach(s => s.Inject(State, Input, Main, Client));

			bulletsTimeout.Inject(entityFactory.QueueDelete);
			bulletsCollisions.Inject(entityFactory.QueueDelete);
			asteroidsCollisions.Inject(entityFactory.QueueDelete);
		}
	}
}