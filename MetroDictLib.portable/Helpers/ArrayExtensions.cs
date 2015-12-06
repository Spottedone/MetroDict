using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroDictLib.Helpers
{
	public static class ArrayExtensions
	{
		public static void Fill<T>(this T[] originalArray, T with)
		{
			for (int i = 0; i < originalArray.Length; i++)
			{
				originalArray[i] = with;
			}
		}

		/// <summary>
		/// Get's a byte array from a point in a source byte array and reverses the bytes. 
		/// Note, if the current platform is not in LittleEndian the input array is assumed to be BigEndian and the bytes are not returned in reverse order
		/// </summary>
		/// <param name="byteArray">The source array to get reversed bytes for</param>
		/// <param name="startIndex">The index in the source array at which to begin the reverse</param>
		/// <param name="count">The number of bytes to reverse</param>
		/// <returns>A new array containing the reversed bytes, or a sub set of the array not reversed.</returns>
		public static byte[] ReverseForBigEndian(this byte[] byteArray, int startIndex, int count)
		{
			if (BitConverter.IsLittleEndian)
				return byteArray.Reverse(startIndex, count);
			else
				return byteArray.SubArray(startIndex, count);

		}

		public static byte[] Reverse(this byte[] byteArray, int startIndex, int count)
		{
			byte[] ret = new byte[count];
			for (int i = startIndex + (count - 1); i >= startIndex; --i)
			{
				byte b = byteArray[i];
				ret[(startIndex + (count - 1)) - i] = b;
			}
			return ret;
		}

		public static byte[] SubArray(this byte[] byteArray, int startIndex, int count)
		{
			byte[] ret = new byte[count];
			for (int i = 0; i < count; ++i)
				ret[0] = byteArray[i + startIndex];
			return ret;
		}

        /// <summary>
        /// Not quite for an array, but whatever.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
                yield return item;
            }
        }
    }
}
