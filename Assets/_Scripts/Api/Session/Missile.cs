using System;

namespace Asteroids
{
	public struct Missile : IColliderEntity
	{
		public Entity Entity { get; private set; }
		public float Radius { get; private set; }
		public PhysicsBody PhysicsBody { get; set; }

		public Missile(Entity entity, RequestMissile req)
		{
			Entity = entity;
			
			Radius = Consts.MissileSize;
			PhysicsBody = req.PhysicsBody;
		}
	}
}