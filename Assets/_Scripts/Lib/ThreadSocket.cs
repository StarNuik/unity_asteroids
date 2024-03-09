using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Asteroids.Lib
{
	public class ThreadSocket : IEventSocket
	{
		private Dictionary<Type, IChannel> channels = new();
		
		public void Subscribe<T>(Action<T> listener)
			where T : struct
		{
			var t = typeof(T);
			Channel<T> chan;

			lock (channels)
			{
				if (!channels.ContainsKey(t))
				{
					chan = new();
					channels[t] = chan;
				}
				else
				{
					chan = channels[t].AsStrict<T>();
				}

				chan.Listeners.Add(listener);
			}
		}

		public void Send<T>(T payload)
			where T : struct
		{
			var t = typeof(T);

			lock (channels)
			{
				if (!channels.ContainsKey(t))
					return;
				
				var chan = channels[t].AsStrict<T>();
				chan.Payload = payload;
			}
		}

		// this one is dangerous
		// bcs any of the server and the client
		// consume ALL of the events
		public void Poll()
		{
			// some nasty locking could occur here
			lock (channels)
			{
				foreach (var (t, iChan) in channels)
				{
					iChan.PollSelf();
				}
			}
		}

		private interface IChannel
		{
			public Channel<T> AsStrict<T>()
				where T : struct;

			public void PollSelf();
		}

		private class Channel<T> : IChannel
			where T : struct
		{
			public T? Payload;
			public List<Action<T>> Listeners { get; private set; } = new();

			public Channel<TTarget> AsStrict<TTarget>()
				where TTarget : struct
			{
				Assert.IsTrue(typeof(TTarget) == typeof(T));

				return this as Channel<TTarget>;
			}

			public void PollSelf()
			{
				if (!Payload.HasValue)
					return;
				
				foreach (var listener in Listeners)
				{
					listener(Payload.Value);
				}

				Payload = null;
			}
		}
	}
}