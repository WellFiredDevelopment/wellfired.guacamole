using System.ComponentModel;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.DataBinding
{
	public class NotifyBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Sets the property if the objects are different (This is in order to prevent recursion with two way binding).
		/// This will return a boolean that states the outcome of the operation.
		/// </summary>
		[PublicAPI]
		protected void SetProperty<T>(ref T storage, T value, string propertyName)
		{
			if (Equals(storage, value))
				return;

			storage = value;
			OnPropertyChanged(propertyName);
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}