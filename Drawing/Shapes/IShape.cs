using System.Collections.Generic;

namespace WellFired.Guacamole.Drawing.Shapes
{
	public interface IShape
	{
		List<Vector> Path { get; set; }
		Side Contains(double x, double y);
		Vector LastPoint();
		Vector FirstPoint();
		void Calculate();
	}
}