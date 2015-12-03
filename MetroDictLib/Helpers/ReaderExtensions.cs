using System;
using System.IO;
using System.Linq;

namespace MetroDictLib.Helpers
{
	public static class ReaderExtensions
	{
		public static UInt32 ReadUInt32BE(this BinaryReader binRdr)
		{
			return BitConverter.ToUInt32(binRdr.ReadBytesRequired(sizeof(UInt32)).ReverseForBigEndian(0, sizeof(Int32)), 0);
		}

		public static Int32 ReadInt32BE(this BinaryReader binRdr)
		{
			return BitConverter.ToInt32(binRdr.ReadBytesRequired(sizeof(Int32)).ReverseForBigEndian(0, sizeof(Int32)), 0);
		}

		public static byte[] ReadBytesRequired(this BinaryReader binRdr, int byteCount)
		{
			var result = binRdr.ReadBytes(byteCount);

			if (result.Length != byteCount)
				throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.", byteCount, result.Length));

			return result;
		}
	}
}
