using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Unit.CompositeCollection
{
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

	public class ObservableGroup : ObservableCollection<GroupEntry>, IDefaultCellContext
	{
		public ObservableGroup(string name)
		{
			CellLabelText = name;
		}

		public bool IsSelected { get; set; }
		public string CellLabelText { get; set; }
	}
}