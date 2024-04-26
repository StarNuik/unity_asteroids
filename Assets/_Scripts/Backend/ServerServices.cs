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
		private PlayerControlsService playerControls = new();
		private PlayerPhysicsService playerPhysics = new();
		private PlayerAttackService playerAttack = new();
		private EntityClampService entityClamp = new();
		private PhysicsService physics = new();
		private BulletsTimeoutService bulletsTimeout = new();
		private CollisionsService collisions = new();
		private BulletsCollisionsService bulletsCollisions = new();
		private AsteroidsCollisionsService asteroidsCollisions = new();

		public void Setup()
		{
			InjectChildren();

			Input.Sub<InputDelta>(deltaService.ApplyInput);

			Main.Sub<RequestAsteroid>(entityFactory.NewAsteroid);
			Main.Sub<RequestBullet>(entityFactory.NewBullet);
			Main.Sub<Collision>(bulletsCollisions.TryCollision);
			Main.Sub<Collision>(asteroidsCollisions.TryCollision);
			
			Main.Sub<Tick>(physics.Tick);
			Main.Sub<Tick>(playerControls.Tick);
			Main.Sub<Tick>(playerPhysics.Tick);
			Main.Sub<Tick>(playerAttack.Tick);
			Main.Sub<Tick>(entityClamp.WarpEntities);
			Main.Sub<Tick>(asteroidTimedSpawn.Tick);
			Main.Sub<Tick>(bulletsTimeout.Tick);
			Main.Sub<Tick>(collisions.Tick);

			Main.Sub<FinishQueued>(entityFactory.FinishDeletes);

			Main.Sub<Sync>(sync.PubUpdates);
		}

		private void InjectChildren()
		{
			var services = new List<Service>() {
				asteroidsCollisions, bulletsCollisions, collisions, bulletsTimeout, physics, entityClamp, playerAttack, playerPhysics, playerControls, sync, deltaService, entityFactory, asteroidTimedSpawn,
			};

			services.ForEach(s => s.Inject(State, Input, Main, Client));

			bulletsTimeout.Inject(entityFactory.QueueDelete);
			bulletsCollisions.Inject(entityFactory.QueueDelete);
			asteroidsCollisions.Inject(entityFactory.QueueDelete);
		}
	}
}