using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Utilities
{
	public static class ColorCompare
	{
		public static UIColor CompareSizeColor(FileSize newSize, FileSize previousSize)
		{
			if (newSize > previousSize)
				return UIColor.FromRGB(136, 0, 43);
			
			return newSize < previousSize ? UIColor.FromRGB(0, 136, 43) : UIColor.FromRGB(40, 40, 40);
		}
	}
}