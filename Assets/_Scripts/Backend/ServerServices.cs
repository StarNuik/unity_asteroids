using System;
using System.Collections.Generic;
using Asteroids.Backend;

namespace Asteroids
{
	public class ServerServices : Service
	{
		private DeltaService deltaService = new();
		private EntityRegistryService entityRegistry = new();
		private EntityFactoryService entityFactory = new();
		private BulletFactoryService bulletFactory = new();
		private AsteroidFactoryService asteroidFactory = new();
		private SyncService sync = new();
		private PlayerControlsService playerControls = new();
		private PlayerPhysicsService playerPhysics = new();
		private PlayerAttackService playerAttack = new();
		private EntityClampService entityClamp = new();

		public void Setup()
		{
			InjectChildren();

			Input.Sub<InputDelta>(deltaService.ApplyInput);

			Main.Sub<EntityCreated>(entityRegistry.RegisterEntity);
			Main.Sub<CreateBullet>(bulletFactory.CreateBullet);
			
			Main.Sub<Tick>(asteroidFactory.Tick);
			Main.Sub<Tick>(playerControls.Tick);
			Main.Sub<Tick>(playerPhysics.Tick);
			Main.Sub<Tick>(playerAttack.Tick);
			Main.Sub<Tick>(entityClamp.WarpEntities);

			Main.Sub<Sync>(sync.PubUpdates);
		}

		private void InjectChildren()
		{
			var services = new List<Service>() {
				entityClamp, playerAttack, playerPhysics, playerControls, sync, deltaService, entityRegistry, entityFactory, bulletFactory,
			};

			services.ForEach(s => s.Inject(State, Input, Main, Client));

			bulletFactory.Inject(entityFactory);
			asteroidFactory.Inject(entityFactory);
		}
	}
}