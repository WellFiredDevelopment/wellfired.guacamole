using WellFired.Guacamole.Data;
using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles
{
	internal static class HeaderButton
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.Clear},
				new Setter {Property = Views.View.OutlineColorProperty, Value = UIColor.Clear},
				new Setter {Property = Views.View.CornerMaskProperty, Value = CornerMask.None},
				new Setter {Property = Views.Button.HorizontalTextAlignProperty, Value = UITextAlign.Start},
				new Setter {Property = Views.Button.VerticalTextAlignProperty, Value = UITextAlign.Middle},
				new Setter {Property = Views.Button.TextColorProperty, Value = UIColor.White}
			}
		};
	}
}