using System;
using System.Collections.Generic;
using Asteroids.Frontend;
using UnityEngine;
using Editor = UnityEngine.SerializeField;

namespace Asteroids
{
	public class MissilesLifetime : MonoBehaviour
	{
		[Editor] MissileObject missilePrefab;

		private ISubscribable Server => Locator.ServerIn;
		private EntitiesHelperService EntitiesHelper => Locator.EntitiesHelper;

		private Dictionary<Entity, MissileObject> missiles = new();

		private void Awake()
		{
			Server.Sub<CreateMissile>(NewMissile);
			Server.Sub<UpdatePhysicsEntity>(TryUpdateMissile);
			Server.Sub<DeleteEntity>(TryDeleteMissile);
		}

		private void NewMissile(CreateMissile msg)
			=> EntitiesHelper.NewEntity(missiles, msg.Missile, missilePrefab);

		private void TryUpdateMissile(UpdatePhysicsEntity update)
			=> EntitiesHelper.TryUpdatePhysics(missiles, update);
		// {
		// 	if (missiles.ContainsKey(update.Entity))
		// 		Debug.Log("[ MissilesLifetime.TryUpdateMissile ]");
		// }

		private void TryDeleteMissile(DeleteEntity msg)
			=> EntitiesHelper.TryDeleteEntity(missiles, msg);
	}
}
