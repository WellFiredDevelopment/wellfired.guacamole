using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.AdjacentLayoutExample
{
	public class AdjacentLayoutTestWindow : Window
	{
		public AdjacentLayoutTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var textEntryStartStart = new TextEntryView
			{
				Text = "h:Start v:Start Align"
			};

			var textEntryMiddleStart = new TextEntryView
			{
				HorizontalTextAlign = UITextAlign.Middle,
				Text = "h:Middle v:Start Align"
			};

			var textEntryEndStart = new TextEntryView
			{
				HorizontalTextAlign = UITextAlign.End,
				Text = "h:Right v:Start Align"
			};

			var textEntryStartMiddle = new TextEntryView
			{
				VerticalTextAlign = UITextAlign.Middle,
				Text = "h:Start v:Middle Align"
			};

			var textEntryMiddleMiddle = new TextEntryView
			{
				HorizontalTextAlign = UITextAlign.Middle,
				VerticalTextAlign = UITextAlign.Middle,
				Text = "h:Middle v:Middle Align"
			};

			var textEntryEndMiddle = new TextEntryView
			{
				HorizontalTextAlign = UITextAlign.End,
				VerticalTextAlign = UITextAlign.Middle,
				Text = "h:Right v:Middle Align"
			};

			var textEntryStartEnd = new TextEntryView
			{
				VerticalTextAlign = UITextAlign.End,
				Text = "h:Start v:End Align"
			};

			var textEntryMiddleEnd = new TextEntryView
			{
				HorizontalTextAlign = UITextAlign.Middle,
				VerticalTextAlign = UITextAlign.End,
				Text = "h:Middle v:End Align"
			};

			var textEntryEndEnd = new TextEntryView
			{
				HorizontalTextAlign = UITextAlign.End,
				VerticalTextAlign = UITextAlign.End,
				Text = "h:Right v:End Align"
			};

			Content = new LayoutView
			{
			    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
			    HorizontalLayout = LayoutOptions.Fill,
				Children =
				{
					textEntryStartStart,
					textEntryMiddleStart,
					textEntryEndStart,
					textEntryStartMiddle,
					textEntryMiddleMiddle,
					textEntryEndMiddle,
					textEntryStartEnd,
					textEntryMiddleEnd,
					textEntryEndEnd
				}
			};
		}
	}
}