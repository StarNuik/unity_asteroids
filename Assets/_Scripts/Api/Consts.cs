using System;
using UnityEngine;

namespace Asteroids
{
	public static class Consts
	{
		public const int ServerTickMs = 16;
		public const float ServerDeltaTime = 1f / 1000f * (float)ServerTickMs;

		// Physics
		public const float WorldDrag = 0.2f;
		
		// Player
		public const float PlayerAcceleration = 0.6f;
		public const float PlayerTopSpeed = 0.5f;
		public const float PlayerAngularSpeed = 270f;
		public const int PrimaryAttackCooldown = (int)(0.2f * ticksPerSecond);
		public const float PlayerRadius = .025f;
		public static Vector2 PlayerStartPosition = Vector2.one * .5f;
		public static Vector2 PlayerStartDirection = Vector2.up;

		// Bullet
		public const float PrimaryBulletSpeed = 0.6f;
		public const int BulletLifeDuration = (int)(2f * ticksPerSecond);
		public const float BulletRadius = 0.01f;

		// Asteroid
		public readonly static Vector2Int AsteroidsTimerRange = new(
			(int)(0.5f * ticksPerSecond),
			(int)(2.0f * ticksPerSecond)
		);
		public readonly static Vector2 AsteroidSpeedRange = new(.125f, .25f);
		public readonly static Vector2 AsteroidSizeRange = new(.05f, .025f);

		//
		private const float ticksPerSecond = 1000f / (float)ServerTickMs;
	}
}