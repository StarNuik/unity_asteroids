namespace Asteroids
{
	public static class Consts
	{
		public static int ServerTickMs = 16;
		public static float ServerDeltaTime = 1f / 1000f * (float)ServerTickMs;
		public static float PlayerAcceleration = 0.2f;
		public static float PlayerTopSpeed = 0.2f;
		public static float PlayerAngularSpeed = 90f;
		public static float WorldDrag = 0.1f;
	}
}