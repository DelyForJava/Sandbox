

using System.Runtime.InteropServices;

namespace NetworkInterface
{
    public class OdaoMessageHeaderId
    {
        public const byte IDENTIFY_VER = 0x05;
        public const byte MESSAGE_VER = 0x03;
        public const byte ENCODE_NONE = 0x00;
        public const byte ENCODE_AES = 0x01;

        public const ushort NM_KEEP_ALIVE = 0xF0F1;
        public const int NAME_LEN = 32;
        public const int PASSWD_LEN = 33;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct OdaoMessageHeader
    {
       public byte identity;
       public byte encode;
       public ushort length;
       public byte version;
       public byte reserve;
       public ushort type;
    };

    public class OdaoMessage : Message {
		public OdaoMessage(ushort route, byte[] data)
		{
            this.route = route;
            this.data = data;
		}
	}
}