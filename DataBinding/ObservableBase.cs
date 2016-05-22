using System.ComponentModel;
using System;

namespace WellFired.Guacamole.Databinding
{
	public class ObservableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
	
		protected bool SetProperty<T>(ref T storage, T value, string propertyName)
		{
			if(Equals (storage, value))
				return false;
	
			storage = value;
			OnPropertyChanged (propertyName);
			return true;
		}
	
		protected void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler == null)
				return;
	
			handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}