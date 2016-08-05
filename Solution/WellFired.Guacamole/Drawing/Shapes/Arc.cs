using System;
using System.Collections.Generic;

namespace WellFired.Guacamole.Drawing.Shapes
{
	public class Arc : IShape
	{
		public double StartAngle;
		public double SweepAngle;
		public Rect ArcRect;
		
		private readonly List<Vector> _path = new List<Vector>();

		public List<Vector> Path
		{
			get { return _path; }
		}

		private double Radius
		{
			get { return ArcRect.Width * 0.5f; }
		}

		public Side Contains(double x, double y)
		{
			return Side.Outside;
		}

		public Vector LastPoint()
		{
			return VectorAtAngle(SweepAngle);
		}

		public Vector FirstPoint()
		{
			return VectorAtAngle(0);
		}

		public void Calculate()
		{
			for(var currentSweep = 0; currentSweep <= SweepAngle; currentSweep++)
				Path.Add(VectorAtAngle(currentSweep));
		}

		private Vector VectorAtAngle(double sweepAngle)
		{
			var currentSweepAngleInRadians = Math.PI / 180 * (sweepAngle + StartAngle);
			var x = ArcRect.Center.X + Radius * Math.Sin(currentSweepAngleInRadians);
			var y = ArcRect.Center.Y + Radius * Math.Cos(currentSweepAngleInRadians);
			return new Vector(x, y);
		}
	}
}