using System;

namespace Asteroids
{
	public struct Entity
	{
		public int Id { get; private set; }

		public Entity(int id)
		{
			Id = id;
		}

		// semantics
		public static bool operator ==(Entity left, Entity right)
		{
			return left.Id.Equals(right.Id);
		}

		public static bool operator !=(Entity left, Entity right)
		{
			return !left.Id.Equals(right.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is Entity && (Entity)obj == this;
		}
	}
}