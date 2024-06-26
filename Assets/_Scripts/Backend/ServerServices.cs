using System;
using System.Collections.Generic;
using Asteroids.Backend;
using Asteroids.Lib;

namespace Asteroids
{
	public class ServerServices : Service
	{
		private new IEventStream Main;
		private ISubscribable Input;
		private IPublisher Client;

		private PlayerInputService playerInput = new();
		private EntityFactoryService entityFactory = new();
		private SyncService sync = new();
		private ActorsMovementService actorsControls = new();
		private ActorsAttackService actorsAttack = new();
		private PhysicsService physics = new();
		private BulletsTimeoutService bulletsTimeout = new();
		private CollisionsService collisions = new();
		private BulletsCollisionsService bulletsCollisions = new();
		private AsteroidsCollisionsService asteroidsCollisions = new();
		private InitService init = new();
		private PlayerScoreService score = new();
		private ObjectsSpawnTimeService objectsSpawnTime = new();
		private ObjectsSpawnPropsService objectsSpawnProps = new();
		private MissilesMovementService missilesMovement = new();
		private MissilesCollisionsService missilesCollisions = new();

		private DebugSyncService debugSync = new();

		public void Inject(
			SessionState state,
			PolledEventStream input,
			IEventStream main,
			IEventStream client)
		{
			base.Inject(state, main);

			Main = main;
			Input = input;
			Client = client;

			InjectChildren();
		}

		public void Setup()
		{
			Input.Sub<UpdateInput>(playerInput.ApplyInput);

			Main.Sub<Init>(init.Init);

			Main.Sub<Tick>(physics.Tick);
			Main.Sub<Tick>(actorsControls.Tick);
			Main.Sub<Tick>(actorsAttack.Tick);
			Main.Sub<Tick>(bulletsTimeout.Tick);
			Main.Sub<Tick>(collisions.Tick);
			Main.Sub<Tick>(objectsSpawnTime.Tick);
			Main.Sub<Tick>(missilesMovement.Tick);

			// subtick
			Main.Sub<RequestActor>(entityFactory.NewActor);
			Main.Sub<RequestAsteroid>(entityFactory.NewAsteroid);
			Main.Sub<RequestBullet>(entityFactory.NewBullet);
			Main.Sub<RequestMissile>(entityFactory.NewMissile);
			Main.Sub<Collision>(bulletsCollisions.TryCollision);
			Main.Sub<Collision>(asteroidsCollisions.TryCollision);
			Main.Sub<Collision>(missilesCollisions.TryCollision);
			Main.Sub<DeleteEntity>(score.TryAddScore);
			Main.Sub<TimeForAsteroid>(objectsSpawnProps.PrepareAsteroid);
			Main.Sub<TimeForMissile>(objectsSpawnProps.PrepareMissile);

			Main.Sub<FinishQueued>(entityFactory.FinishDeletes);

			Main.Sub<Sync>(debugSync.PubDebug);
			Main.Sub<Sync>(sync.PubUpdates);
			Main.Sub<DeleteEntity>(sync.Rebroadcast);
			Main.Sub<UpdateHud>(sync.Rebroadcast);
			Main.Sub<CreateActor>(sync.Rebroadcast);
			Main.Sub<CreateAsteroid>(sync.Rebroadcast);
			Main.Sub<CreateBullet>(sync.Rebroadcast);
			Main.Sub<CreateMissile>(sync.Rebroadcast);
		}

		private void InjectChildren()
		{
			var services = new List<Service>() { missilesCollisions, missilesMovement, objectsSpawnProps, objectsSpawnTime, score, init, debugSync, asteroidsCollisions, bulletsCollisions, collisions, bulletsTimeout, physics, actorsAttack, actorsControls, sync, playerInput, entityFactory, };

			services.ForEach(s => s.Inject(State, Main));

			sync.Inject(Client);

			bulletsTimeout.Inject(entityFactory.QueueDelete);
			bulletsCollisions.Inject(entityFactory.QueueDelete);
			asteroidsCollisions.Inject(entityFactory.QueueDelete);
			missilesCollisions.Inject(entityFactory.QueueDelete);
		}
	}
}