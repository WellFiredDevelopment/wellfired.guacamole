using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.ComplexLayoutExample
{
	public class ComplexLayoutExampleWindow : Window
	{
		private static readonly UIColor DarkerBackgroundColor = UIColor.FromRGB(50, 50, 50);
		private static readonly UIColor ButtonBorder = UIColor.FromRGB(88, 88, 88);

		public ComplexLayoutExampleWindow()
		{
			Padding = new UIPadding(5, 5, 5, 5);

			var header = new LayoutView
			{
				BackgroundColor = UIColor.Clear,
				Spacing = 5,
			    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
			    Children =
				{
					new LayoutView
					{
						BackgroundColor = DarkerBackgroundColor,
					    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
					    Spacing = 5,
						Padding = 4,
						CornerRadius = 8.0,
						Children =
						{
							new LayoutView
							{
								BackgroundColor = ButtonBorder,
							    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
							    Padding = 2,
								Spacing = 3,
								CornerRadius = 8.0,
								Children =
								{
									new Button
									{
										CornerRadius = 8.0,
										CornerMask = CornerMask.Left,
										Text = "New"
									},
									new Button
									{
										CornerRadius = 8.0,
										CornerMask = CornerMask.Right,
										Text = "Open"
									}
								}
							}
						}
					},
					new LayoutView
					{
						BackgroundColor = DarkerBackgroundColor,
					    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
					    Spacing = 5,
						Padding = 4,
						CornerRadius = 8.0,
						Children =
						{
							new LayoutView
							{
								BackgroundColor = ButtonBorder,
							    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
							    Padding = 2,
								Spacing = 3,
								CornerRadius = 8.0,
								Children =
								{
									new Label
									{
										Text = "Id",
										CornerRadius = 8.0,
										CornerMask = CornerMask.Left
									},
									new TextEntry
									{
										Text = "Sequence",
										CornerRadius = 8.0,
										CornerMask = CornerMask.Right
									}
								}
							},
							new LayoutView
							{
								BackgroundColor = ButtonBorder,
							    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
							    Padding = 2,
								Spacing = 3,
								CornerRadius = 8.0,
								Children =
								{
									new Label
									{
										Text = "Duration",
										CornerRadius = 8.0,
										CornerMask = CornerMask.Left
									},
									new NumberEntry
									{
										Number = 10,
										CornerRadius = 8.0,
										CornerMask = CornerMask.Right
									}
								}
							},
							new LayoutView
							{
								BackgroundColor = ButtonBorder,
							    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
							    Padding = 2,
								Spacing = 3,
								CornerRadius = 8.0,
								Children =
								{
									new Button
									{
										CornerRadius = 8.0,
										CornerMask = CornerMask.Left,
										Text = "Duplicate"
									},
									new Button
									{
										CornerRadius = 8.0,
										CornerMask = CornerMask.Right,
										Text = "Prefab"
									}
								}
							}
						}
					}
				}
			};

			Content = header;
		}
	}
}