namespace Asteroids
{
	public static class Consts
	{
		public static int ServerTickMs = 16;
		public static float ServerDeltaTime = 1f / 1000f * (float)ServerTickMs;

		public static float WorldDrag = 0.1f;
		
		public static float PlayerAcceleration = 0.4f;
		public static float PlayerTopSpeed = 0.25f;
		public static float PlayerAngularSpeed = 180f;
	}
}