using System;
using System.Threading;
using System.Threading.Tasks;
using JamesFrowen.SimpleWeb;

namespace Asteroids
{
	public class Server
	{
		private CancellationToken cancel;
		private SimpleWebServer socket;
		private ulong tick = 0;

		public async void EntryPoint(CancellationToken cancel)
		{
			this.cancel = cancel;

			socket = new SimpleWebServer(5000, Consts.TcpConfig, ushort.MaxValue, 5000, new());
			socket.onData += OnSocketData;
			socket.Start(Consts.ServerPort);

			while (!cancel.IsCancellationRequested)
				await Tick();
		}

		private void OnSocketData(int _, ArraySegment<byte> data)
		{}

		private async Task Tick()
		{
			socket.ProcessMessageQueue();

			var m = new Message();
			m.Tick = tick;
			socket.SendOne(1, Message.ToBytes(m));

			await Task.Delay(16);
			tick++;
		}
	}
}