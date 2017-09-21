using System;
using System.Net.Sockets;

namespace NetworkInterface
{
    public class OdaoTransporter : Transporter
    {
		public OdaoTransporter(Socket socket, Action<byte[]> processer)
			:base(socket,processer)
        {
            HeadLength = 8;
        }

        override protected void OnParseHead()
        {
            OdaoMessageHeader omh;

            omh.identity = headBuffer[0];
            omh.encode = headBuffer[1];

            short msglen = BitConverter.ToInt16(headBuffer, 2);
            msglen = System.Net.IPAddress.NetworkToHostOrder(msglen);
            omh.length = (ushort)msglen;

            omh.version = headBuffer[4];
            omh.reserve = headBuffer[5];

            short msgtype = BitConverter.ToInt16(headBuffer, 6);
            msgtype = System.Net.IPAddress.NetworkToHostOrder(msgtype);
            omh.type = (ushort)msgtype;

            pkgLength = omh.length - 8;

            //Console.WriteLine("Odao Message Header");
            //Console.WriteLine("Odao Message identity : {0}", omh.identity);
            //Console.WriteLine("Odao Message encode : {0}", omh.encode);
            //Console.WriteLine("Odao Message length : {0}", omh.length);
            //Console.WriteLine("Odao Message version : {0}", omh.version);
            //Console.WriteLine("Odao Message reserve : {0}", omh.reserve);
            Console.Write("Odao Message type : 0x{0:x},{1}\t", omh.type, pkgLength);
        }
    }
}