using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
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
					new GroupEntry("Amelia"),
					new GroupEntry("Alfie"),
					new GroupEntry("Archie")
				},
				new Group("B") {
					new GroupEntry("Brooke"),
					new GroupEntry("Bobby"),
					new GroupEntry("Bella"),
					new GroupEntry("Ben"),
					new GroupEntry("Bump")
				},
				new Group("C") {
					new GroupEntry("Calvin"),
					new GroupEntry("Calum"),
					new GroupEntry("Collin"),
					new GroupEntry("Cornelius")
				},
				new Group("D") {
					new GroupEntry("Darren"),
					new GroupEntry("David"),
					new GroupEntry("Dennis"),
				},
	            new Group("E") {
		            new GroupEntry("Elvis"),
		            new GroupEntry("Evelyn")
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

    public class GroupEntry : IDefaultCellContext
    {
        public GroupEntry(string name)
        {
            CellLabelText = name;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate(object sender, PropertyChangedEventArgs args) {  };
        public bool IsSelected { get; set; }
        public string CellLabelText { get; set; }
    }

    public class Group : List<GroupEntry>, IDefaultCellContext
    {
        public Group(string name)
        {
            CellLabelText = name;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate(object sender, PropertyChangedEventArgs args) {  };
        public bool IsSelected { get; set; }
        public string CellLabelText { get; set; }
    }
}