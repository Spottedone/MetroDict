using System;
using System.IO;

namespace MetroDictLib.Helpers
{
    public static class ReaderExtensions
    {
        public static uint ReadUInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToUInt32(binRdr.ReadBytesRequired(sizeof (uint)).ReverseForBigEndian(0, sizeof (int)), 0);
        }

        public static int ReadInt32BE(this BinaryReader binRdr)
        {
            return BitConverter.ToInt32(binRdr.ReadBytesRequired(sizeof (int)).ReverseForBigEndian(0, sizeof (int)), 0);
        }

        public static byte[] ReadBytesRequired(this BinaryReader binRdr, int byteCount)
        {
            var result = binRdr.ReadBytes(byteCount);

            if (result.Length != byteCount)
                throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.",
                    byteCount, result.Length));

            return result;
        }
    }
}