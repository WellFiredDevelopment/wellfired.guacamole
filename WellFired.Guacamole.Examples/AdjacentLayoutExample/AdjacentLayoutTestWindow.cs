namespace WellFired.Guacamole.Examples.AdjacentLayoutTest
{
	public class AdjacentLayoutTestWindow : Window
	{
		public AdjacentLayoutTestWindow()
		{
			Padding = new UIPadding(5);
			
			var textEntryStartStart = new TextEntry
			{
				BackgroundColor = UIColor.White,
				Text = "h:Start v:Start Align"
			};

			var textEntryMiddleStart = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.Middle,
				BackgroundColor = UIColor.White,
				Text = "h:Middle v:Start Align"
			};

			var textEntryEndStart = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.End,
				BackgroundColor = UIColor.White,
				Text = "h:Right v:Start Align"
			};

			var textEntryStartMiddle = new TextEntry
			{
				VerticalTextAlign = UITextAlign.Middle,
				BackgroundColor = UIColor.White,
				Text = "h:Start v:Middle Align"
			};

			var textEntryMiddleMiddle = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.Middle,
				VerticalTextAlign = UITextAlign.Middle,
				BackgroundColor = UIColor.White,
				Text = "h:Middle v:Middle Align"
			};

			var textEntryEndMiddle = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.End,
				VerticalTextAlign = UITextAlign.Middle,
				BackgroundColor = UIColor.White,
				Text = "h:Right v:Middle Align"
			};

			var textEntryStartEnd = new TextEntry
			{
				VerticalTextAlign = UITextAlign.End,
				BackgroundColor = UIColor.White,
				Text = "h:Start v:End Align"
			};

			var textEntryMiddleEnd = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.Middle,
				VerticalTextAlign = UITextAlign.End,
				BackgroundColor = UIColor.White,
				Text = "h:Middle v:End Align"
			};

			var textEntryEndEnd = new TextEntry
			{
				HorizontalTextAlign = UITextAlign.End,
				VerticalTextAlign = UITextAlign.End,
				BackgroundColor = UIColor.White,
				Text = "h:Right v:End Align"
			};

			Content = new AdjacentLayout
			{
				Spacing = 5,
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