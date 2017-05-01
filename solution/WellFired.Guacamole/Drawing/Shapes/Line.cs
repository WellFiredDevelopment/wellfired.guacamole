using System;
using System.Collections.Generic;

namespace WellFired.Guacamole.Drawing.Shapes
{
	public class Line : IShape
	{
		public Vector EndPoint;
		public Vector StartPoint;

		public List<Vector> Path { get; private set; }

		public Side Contains(double x, double y)
		{
			return Side.Inside;
		}

		public Vector LastPoint()
		{
			return EndPoint;
		}

		public Vector FirstPoint()
		{
			return StartPoint;
		}

		public void Calculate()
		{
			Path = new List<Vector>(90);

			var toEnd = EndPoint - StartPoint;
			var direction = Vector.Normalize(toEnd);
			var absDistance = Math.Abs(toEnd.Length);

			for (var distanceStep = 0.0; distanceStep < absDistance; distanceStep += 0.5)
			{
				var newPathPoint = StartPoint + direction*distanceStep;
				Path.Add(newPathPoint);
			}
		}
	}
}