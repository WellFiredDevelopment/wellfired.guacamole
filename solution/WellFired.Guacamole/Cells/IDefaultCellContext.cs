using System.ComponentModel;

namespace WellFired.Guacamole.Cells
{
	public interface IDefaultCellContext : INotifyPropertyChanged
	{
		bool IsSelected { get; set; }
		string CellLabelText { get; set; }
	}
}