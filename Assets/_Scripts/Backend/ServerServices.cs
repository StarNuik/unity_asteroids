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

		public void Setup()
		{
			InjectChildren();

			Input.Sub<InputDelta>(deltaService.ApplyInput);

			Main.Sub<EntityCreated>(entityRegistry.RegisterEntity);
			Main.Sub<CreateBullet>(bulletFactory.CreateBullet);
			
			Main.Sub<Tick>(asteroidFactory.Tick);
			PlayerControlsService.Sub(Main);
			PlayerPhysicsService.Sub(Main);
			PlayerAttackService.Sub(Main);
			EntityClampService.Sub(Main);

			Main.Sub<Sync>(sync.PubUpdates);
		}

		private void InjectChildren()
		{
			var services = new List<Service>() {
				sync, deltaService, entityRegistry, entityFactory, bulletFactory,
			};

			services.ForEach(s => s.Inject(State, Input, Main, Client));

			bulletFactory.Inject(entityFactory);
			asteroidFactory.Inject(entityFactory);
		}
	}
}