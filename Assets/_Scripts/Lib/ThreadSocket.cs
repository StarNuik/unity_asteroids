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
			var chan = GetChannelSafe<T>();
			lock (chan)
			{
				chan.Listeners.Add(listener);
			}
		}

		public void Send<T>(T payload)
			where T : struct
		{
			var chan = GetChannelSafe<T>();

			lock (chan)
			{
				chan.Messages.Add(payload);
			}
		}

		public void Poll<T>()
			where T : struct
		{
			var chan = GetChannelSafe<T>();

			lock (chan)
			{
				chan.PollSelf();
			}
		}

		private Channel<T> GetChannelSafe<T>()
			where T : struct
		{
			Channel<T> chan;
			var t = typeof(T);

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
			}

			return chan;
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
			public List<T> Messages = new();
			public List<Action<T>> Listeners { get; private set; } = new();

			public Channel<TTarget> AsStrict<TTarget>()
				where TTarget : struct
			{
				Assert.IsTrue(typeof(TTarget) == typeof(T));

				return this as Channel<TTarget>;
			}

			public void PollSelf()
			{
				foreach (var message in Messages)
				{
					foreach (var listener in Listeners)
					{
						listener(message);
					}
				}

				Messages.Clear();
			}
		}
	}
}