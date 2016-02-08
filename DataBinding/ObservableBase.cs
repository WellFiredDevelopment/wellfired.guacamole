using System.ComponentModel;
using System;

namespace WellFired.Guacamole.Databinding
{
	public class ObservableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
	
		/// <summary>
		/// Sets the property if the objects are different (This is in order to prevent recursion with two way binding).
		/// This will return a boolean that states the outcome of the operation.
		/// </summary>
		protected bool SetProperty<T>(ref T storage, T value, string propertyName)
		{
			if(Object.Equals (storage, value))
				return false;
	
			storage = value;
			OnPropertyChanged (propertyName);
			return true;
		}
	
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler == null)
				return;
	
			handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}