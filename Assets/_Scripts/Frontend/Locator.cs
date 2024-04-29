using Asteroids.App;
using Asteroids.Lib;

namespace Asteroids.Frontend
{
	public static class Locator
	{
		public static ServerConnection Connection;
		public static ISubscribable ServerIn => Connection.ServerIn;
		public static IPublisher ClientOut => Connection.ClientOut;

		public static FieldService Field;
		public static EntitiesHelperService EntitiesHelper;
	}
}