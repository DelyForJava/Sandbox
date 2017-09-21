using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NetworkInterface
{
	public class OdaoProtocol : Protocol
    {
		OdaoMessageProtocol messageProtocol;

		public OdaoProtocol(SocketClient sc, Socket socket)
			:base(sc,socket)
        {
			this.transporter = new OdaoTransporter(socket, this.processMessage);

            messageProtocol = new OdaoMessageProtocol();
            this.state = ProtocolState.working;
        }
		
		public void send(ushort route, uint id, byte[] msg)
		{
			if (this.state != ProtocolState.working)
                return;
		
			byte[] body = messageProtocol.encode(route, id, msg);

            if (this.state == ProtocolState.closed)
                return;

            transporter.send(body);
        }

		public void sendMP(ushort route, uint id, byte[] msg)
		{
			if (this.state != ProtocolState.working)
				return;

			byte[] body = messageProtocol.encodeMP(route, id, msg);

			if (this.state == ProtocolState.closed)
				return;

			transporter.send(body);
		}

		override protected void processMessage(byte[] bytes)
		{
            _client.processMessage(messageProtocol.decode(bytes));
        }
    }
}
