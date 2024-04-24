using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Asteroids.Lib
{
	public class PolledEventStream : IEventStream
	{
		private Dictionary<Type, Channel> channels = new();
		
		public void Sub<T>(Action<T> listener)
		{
			var chan = GetChannel(typeof(T));
			chan.Sub(Wrap(listener));
		}

		public void Sub<T>(Action listener)
			=> Sub<T>(_ => listener());

		public void Pub<T>(T payload)
		{
			var t = typeof(T);
			
			{
				var chan = GetChannel(t);
				chan.Pub(payload);
			}

			foreach (var tt in t.GetInterfaces())
			{
				var chan = GetChannel(tt);
				chan.Pub(payload);
			}
		}

		public void Poll()
		{
			foreach (var (_, chan) in channels)
			{
				chan.Poll();
			}
		}

		private Channel GetChannel(Type t)
		{
			if (channels.ContainsKey(t))
			{
				return channels[t];
			}
			else
			{
				var chan = new Channel();
				channels[t] = chan;
				return chan;
			}
		}

		private Action<object> Wrap<T>(Action<T> callback)
		{
			return obj => callback((T)obj);
		}

		private class Channel
		{
			private List<object> messages = new();
			private List<Action<object>> listeners = new();

			public void Sub(Action<object> listener)
			{
				listeners.Add(listener);
			}

			public void Pub(object payload)
			{
				messages.Add(payload);
			}

			public void Poll()
			{
				foreach (var m in messages)
				{
					foreach (var l in listeners)
					{
						l(m);
					}
				}
				messages.Clear();
			}
		}
	}
}