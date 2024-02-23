using JamesFrowen.SimpleWeb;


namespace Asteroids
{
	public static class Consts
	{
		public static TcpConfig TcpConfig = new(false, 5000, 20000);

		public static ushort ServerPort = 12345;
		public static string ServerAddress = $"ws://127.0.0.1:{ServerPort}";
	}
}