using System;
using System.Text;

namespace NetworkInterface
{
    // MsgHeadDef + DATA
    public class OdaoMessageProtocol : MessageProtocol {

		public OdaoMessageProtocol() {}

		public byte[] encode(ushort route, byte[] msg)
		{
			return encode(route, 0, msg);
		}

		public byte[] encode(ushort route, uint id, byte[] msg)
		{
            byte[] bytes = new byte[msg.Length];

            OdaoMessageHeader omh;
            omh.identity = OdaoMessageHeaderId.IDENTIFY_VER;
            omh.encode = OdaoMessageHeaderId.ENCODE_NONE;
            omh.length = (ushort)(msg.Length);
            omh.version = OdaoMessageHeaderId.MESSAGE_VER;
            omh.reserve = 0;
            omh.type = route;

            //byte[] header = XConvert.ToByte(omh);
            byte[] header = XConvert.ConvertToByte(omh, 0);
            writeBytes(header, 0, bytes);

            short len = System.Net.IPAddress.HostToNetworkOrder((short)omh.length);
            byte[] msgLength = BitConverter.GetBytes(len);
            Array.Copy(msgLength, 0, bytes, 2, msgLength.Length);

            short type = System.Net.IPAddress.HostToNetworkOrder((short)omh.type);
            byte[] msgType = BitConverter.GetBytes(type);
            Array.Copy(msgType, 0, bytes, 6, msgType.Length);

            //Console.WriteLine("{0:x},{1:x},{2:x},{3:x},{4:x},{5:x},{6:x},{7:x}", bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]);

            Array.Copy(msg, 8, bytes, 8, msg.Length - 8);
            //writeBytes(msg, 8, bytes);

            return bytes;
		}

		public byte[] encodeMP(ushort route, byte[] msg)
		{
			return encodeMP(route, 0, msg);
		}

		public byte[] encodeMP(ushort route, uint id, byte[] msg)
		{
			byte[] bytes = new byte[msg.Length + 8];

			OdaoMessageHeader omh;
			omh.identity = OdaoMessageHeaderId.IDENTIFY_VER;
			omh.encode = OdaoMessageHeaderId.ENCODE_NONE;
			omh.length = (ushort)(msg.Length + 8);
			omh.version = OdaoMessageHeaderId.MESSAGE_VER;
			omh.reserve = 0;
			omh.type = route;

			//byte[] header = XConvert.ToByte(omh);
			byte[] header = XConvert.ConvertToByte(omh, 0);
			writeBytes(header, 0, bytes);

			short len = System.Net.IPAddress.HostToNetworkOrder((short)omh.length);
			byte[] msgLength = BitConverter.GetBytes(len);
			Array.Copy(msgLength, 0, bytes, 2, msgLength.Length);

			short type = System.Net.IPAddress.HostToNetworkOrder((short)omh.type);
			byte[] msgType = BitConverter.GetBytes(type);
			Array.Copy(msgType, 0, bytes, 6, msgType.Length);

			//Console.WriteLine("{0:x},{1:x},{2:x},{3:x},{4:x},{5:x},{6:x},{7:x}", bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]);

			Array.Copy(msg, 0, bytes, 8, msg.Length);
			//writeBytes(msg, 8, bytes);

			return bytes;
		}

		public OdaoMessage decode(byte[] buffer)
		{
            ushort route = readShort(6, buffer);
			byte[] msg = new byte[buffer.Length - 8];
			Array.Copy (buffer, 8, msg, 0, buffer.Length - 8);
            return new OdaoMessage(route, msg);
		}
	}
}
