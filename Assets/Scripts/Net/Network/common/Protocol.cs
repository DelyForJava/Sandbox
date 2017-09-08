using System;
using SimpleJson;
using System.Text;

namespace NetworkInterface
{
	public class Protocol
	{
        protected ProtocolState state;
		protected Transporter transporter;
		protected SocketClient _client;

        public SocketClient getClient()
		{
			return _client;
		}

		public Protocol(SocketClient client, System.Net.Sockets.Socket socket)
		{
            _client = client;
			this.transporter = new Transporter(socket, this.processMessage);
			this.transporter.onDisconnect = onDisconnect;
			this.state = ProtocolState.start;
		}

        virtual public void start(JsonObject user, Action<JsonObject> callback)
        {
            this.transporter.start();
        }

		virtual protected void processMessage(byte[] bytes) {}

		private void onDisconnect()
		{
            _client.Disconnect();
		}

		virtual public void close()
		{
			Console.WriteLine ("Protocal close...");
			transporter.close();
			this.state = ProtocolState.closed;
		}
	}
}
