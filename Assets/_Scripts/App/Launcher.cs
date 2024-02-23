
using System;
using System.Threading;
using UnityEngine;

namespace Asteroids
{
	public class Launcher : MonoBehaviour
	{
		private CancellationTokenSource cancel = new();
		
		public void Awake()
		{
			var s = new Server();
			s.EntryPoint(cancel.Token);
		}
	}
}
