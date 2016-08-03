using System.Collections.Generic;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Drawing.Shapes
{
    [PublicAPI]
	public interface IShape
	{
		List<Vector> Path { get; }
		[UsedImplicitly]
		Side Contains(double x, double y);
		[UsedImplicitly]
		Vector LastPoint();
		[UsedImplicitly]
		Vector FirstPoint();
		void Calculate();
	}
}