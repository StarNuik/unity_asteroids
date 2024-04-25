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

		// public bool Equals(Entity other)
		// {
		// 	return other.Id == this.Id;
		// }
		
		public static bool operator ==(Entity left, Entity right)
		{
			return left.Id.Equals(right.Id);
		}

		public static bool operator !=(Entity left, Entity right)
		{
			return !left.Id.Equals(right.Id);
		}
	}
}