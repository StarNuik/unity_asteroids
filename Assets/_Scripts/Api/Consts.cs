using System;
using UnityEngine;

namespace Asteroids
{
	public static class Consts
	{
		public const int ServerTickMs = 16;
		public const float ServerDeltaTime = 1f / 1000f * (float)ServerTickMs;

		public const float WorldDrag = 0.1f;
		
		public const float PlayerAcceleration = 0.4f;
		public const float PlayerTopSpeed = 0.5f;
		public const float PlayerAngularSpeed = 180f;
		
		public const int PrimaryAttackCooldown = (int)(0.3f * ticksPerSecond);
		public const float PrimaryBulletSpeed = 2.0f;

		private const float ticksPerSecond = 1000f / (float)ServerTickMs;
	}
}