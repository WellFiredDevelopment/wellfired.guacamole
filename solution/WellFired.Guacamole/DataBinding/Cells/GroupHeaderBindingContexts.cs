using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WellFired.Guacamole.Cells;

namespace WellFired.Guacamole.DataBinding.Cells
{
	public class GroupHeaderBindingContext<TEntryType> : List<TEntryType>, IDefaultCellContext
	{
		private string _cellLabelText;
		private bool _isSelected;
		public event PropertyChangedEventHandler PropertyChanged;

		public GroupHeaderBindingContext(string cellLabelText)
		{
			CellLabelText = cellLabelText;
		}

		public bool IsSelected
		{
			get => _isSelected;
			set => _isSelected = value;
		}
		
		public string CellLabelText
		{
			get => _cellLabelText;
			set => _cellLabelText = value;
		}

		protected void SetProperty<TPropertyType>(ref TPropertyType storage, TPropertyType value, [CallerMemberName] string propertyName = @"")
		{
			if (Equals(storage, value))
				return;

			storage = value;
			OnPropertyChanged(propertyName);
		}

		private void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}