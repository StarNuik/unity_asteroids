using System;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace Asteroids
{
	public struct Message
	{
		public ulong Tick;

		public static byte[] ToBytes(Message m)
		{
			var s = JsonConvert.SerializeObject(m);
			return Encoding.ASCII.GetBytes(s);
		}

		public static Message FromBytes(byte[] bytes)
		{
			var s = Encoding.ASCII.GetString(bytes);
			return JsonConvert.DeserializeObject<Message>(s);
		}

		public static Message FromSegment(ArraySegment<byte> segm)
		{
			var s = Encoding.ASCII.GetString(segm.Array, segm.Offset, segm.Count);
			return JsonConvert.DeserializeObject<Message>(s);
		}
	}
}