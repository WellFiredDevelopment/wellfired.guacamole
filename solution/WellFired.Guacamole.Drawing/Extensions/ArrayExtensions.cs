using System;

namespace WellFired.Guacamole.Drawing.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// This fills a ray with a given value and is faster than a single for loop.
        /// </summary>
        /// <param name="destinationArray"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void FastFill<T>(this T[] destinationArray, params T[] value)
        {
            if (destinationArray == null)
                throw new ArgumentNullException(nameof(destinationArray));

            if (value.Length >= destinationArray.Length)
                throw new ArgumentException("Length of value array must be less than length of destination");

            // set the initial array value
            Buffer.BlockCopy(value, 0, destinationArray, 0, value.Length); 

            var arrayToFillHalfLength = destinationArray.Length / 2;
            int copyLength;

            for(copyLength = value.Length; copyLength < arrayToFillHalfLength; copyLength <<= 1)
                Buffer.BlockCopy(destinationArray, 0, destinationArray, copyLength, copyLength);

            Buffer.BlockCopy(destinationArray, 0, destinationArray, copyLength, destinationArray.Length - copyLength);
        }
    }
}