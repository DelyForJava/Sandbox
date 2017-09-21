using System;
using System.Text;
using System.Collections.Generic;

namespace NetworkInterface
{
	public abstract class MessageProtocol
	{
		protected void writeInt(int offset, uint value, byte[] bytes)
		{
			bytes[offset] = (byte)(value >> 24 & 0xff);
			bytes[offset + 1] = (byte)(value >> 16 & 0xff);
			bytes[offset + 2] = (byte)(value >> 8 & 0xff);
			bytes[offset + 3] = (byte)(value & 0xff);
		}

		protected uint readInt(int offset, byte[] bytes)
		{
			ushort result = 0;

			result += (ushort)(bytes[offset] << 24);
			result += (ushort)(bytes[offset + 1] << 16);
			result += (ushort)(bytes[offset + 2] << 8);
			result += (ushort)(bytes[offset + 3]);

			return result;
		}

		protected void writeShort(int offset, ushort value, byte[] bytes)
		{
			bytes[offset] = (byte)(value >> 8 & 0xff);
			bytes[offset + 1] = (byte)(value & 0xff);
		}

		protected ushort readShort(int offset, byte[] bytes)
		{
			ushort result = 0;

			result += (ushort)(bytes[offset] << 8);
			result += (ushort)(bytes[offset + 1]);

			return result;
		}

		protected int byteLength(string msg)
		{
			return Encoding.UTF8.GetBytes(msg).Length;
		}

		protected void writeBytes(byte[] source, int offset, byte[] target)
		{
			for (int i = 0; i < source.Length; i++)
			{
				target[offset + i] = source[i];
			}
		}
	}
}
