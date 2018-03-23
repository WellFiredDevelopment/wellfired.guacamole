using System;

namespace WellFired.Guacamole.Data
{
	public class MathUtil
	{
		/// <summary>
		/// Returns true if a is almost the same value as b, using a d of 0.0001f
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool NearEqual(float a, float b)
		{
			return Math.Abs(a - b) < 0.0001f;
		}
	}
}