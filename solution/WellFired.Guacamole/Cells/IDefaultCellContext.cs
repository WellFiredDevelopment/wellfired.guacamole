using System.ComponentModel;

namespace WellFired.Guacamole.Cells
{
	public interface IDefaultCellContext : ISelectableCell, INotifyPropertyChanged
	{
		string CellLabelText { get; set; }
	}
}