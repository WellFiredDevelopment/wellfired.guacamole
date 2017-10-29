using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ListViewGroupExample
{
	public class ListViewGroupTestWindow : Window
	{
		public ListViewGroupTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider)
			: base(logger, persistantData, platformProvider)
		{
			var itemSource = new List<Group> {
				new Group("A") {
					new LabelCellBindingContext("Amelia"),
					new LabelCellBindingContext("Alfie"),
					new LabelCellBindingContext("Archie")
				},
				new Group("B") {
					new LabelCellBindingContext("Brooke"),
					new LabelCellBindingContext("Bobby"),
					new LabelCellBindingContext("Bella"),
					new LabelCellBindingContext("Ben"),
					new LabelCellBindingContext("Bump")
				},
				new Group("C") {
					new LabelCellBindingContext("Calvin"),
					new LabelCellBindingContext("Calum"),
					new LabelCellBindingContext("Collin"),
					new LabelCellBindingContext("Cornelius")
				},
				new Group("D") {
					new LabelCellBindingContext("Darren"),
					new LabelCellBindingContext("David"),
					new LabelCellBindingContext("Dennis"),
				},
				new Group("E") {
					new LabelCellBindingContext("Elvis"),
					new LabelCellBindingContext("Evelyn")
				}
			};

			Content = new ListView {
				BackgroundColor = UIColor.White,
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				Orientation = OrientationOptions.Vertical,
				Spacing = 2,
				EntrySize = 30,
				HeaderSize = 60,
				ItemSource = itemSource
			};
		}
	}

	public class Group : List<LabelCellBindingContext>, IDefaultCellContext
	{
		public Group(string name)
		{
			CellLabelText = name;
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate {  };
		public bool IsSelected { get; set; }
		public string CellLabelText { get; set; }
	}
}