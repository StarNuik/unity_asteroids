using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Asteroids
{
	public class PhysicsService : Service
	{
		public void Tick()
		{
			UpdateAll(State.Bullets);
			UpdateAll(State.Asteroids);
		}

		private void UpdateAll<T>(Dictionary<Entity, T> items)
			where T : IPhysicsBody
		{
			var entities = items.Keys.ToList();
			for (int i = 0; i < entities.Count; i++)
			{
				var entity = entities[i];
				var item = items[entity];
				var from = item.PhysicsBody;
				item.PhysicsBody = UpdateBody(from);
				items[entity] = item;
			}
		}

		private PhysicsBody UpdateBody(PhysicsBody body)
		{
			body = Move(body);
			body = ClampPosition(body);
			return body;
		}

		private PhysicsBody Move(PhysicsBody body)
		{
			var vel = body.Velocity;
			body.Position += vel * Consts.ServerDeltaTime;
			return body;
		}

		private PhysicsBody ClampPosition(PhysicsBody body)
		{
			var pos = body.Position;
			pos.x = Mathf.Repeat(pos.x, 1f);
			pos.y = Mathf.Repeat(pos.y, 1f);
			body.Position = pos;
			return body;
		}

		// private void Drag()
		// {
		// 	var vel = State.PlayerVelocity;
		// 	var dir = vel.normalized;
		// 	var len = vel.magnitude;

		// 	var next = Mathf.Max(0f, len - Consts.WorldDrag * Consts.ServerDeltaTime);
		// 	State.PlayerVelocity = dir * next;
		// }
	}
}