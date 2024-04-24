using System;
using System.Collections.Generic;
using Asteroids.Backend;

namespace Asteroids
{
	public class ServerServices : Service
	{
		private DeltaService deltaService = new();
		private EntityFactoryService entityFactory = new();
		private AsteroidTimerService asteroidTimer = new();
		private SyncService sync = new();
		private PlayerControlsService playerControls = new();
		private PlayerPhysicsService playerPhysics = new();
		private PlayerAttackService playerAttack = new();
		private EntityClampService entityClamp = new();
		private PhysicsService physics = new();

		public void Setup()
		{
			InjectChildren();

			Input.Sub<InputDelta>(deltaService.ApplyInput);

			Main.Sub<PlayerAttack>(
				_ => entityFactory.NewBullet()
			);
			
			Main.Sub<Tick>(physics.Tick);
			Main.Sub<Tick>(playerControls.Tick);
			Main.Sub<Tick>(playerPhysics.Tick);
			Main.Sub<Tick>(playerAttack.Tick);
			Main.Sub<Tick>(entityClamp.WarpEntities);
			Main.Sub<Tick>(asteroidTimer.Tick);

			Main.Sub<Sync>(sync.PubUpdates);
		}

		private void InjectChildren()
		{
			var services = new List<Service>() {
				physics, entityClamp, playerAttack, playerPhysics, playerControls, sync, deltaService, entityFactory, asteroidTimer,
			};

			services.ForEach(s => s.Inject(State, Input, Main, Client));

			asteroidTimer.Inject(entityFactory);
		}
	}
}