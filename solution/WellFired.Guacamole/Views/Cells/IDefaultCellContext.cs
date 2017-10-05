using System.ComponentModel;

namespace WellFired.Guacamole.Views.Cells
{
	public interface IDefaultCellContext : INotifyPropertyChanged
	{
		bool IsSelected { get; set; }
	}
}