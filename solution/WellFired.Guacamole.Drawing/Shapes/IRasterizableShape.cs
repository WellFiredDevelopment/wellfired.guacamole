using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Drawing.Shapes
{
	[PublicAPI]
	public interface IRasterizableShape
	{
		void Rasterize(UIImageRaw image, UIColor color);
		void RasterizeWithAA(UIImageRaw image, UIColor color);
		void RasterizeWithWidthAndAA(UIImageRaw image, UIColor color);
	}
}