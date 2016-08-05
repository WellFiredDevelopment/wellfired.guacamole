using System.ComponentModel;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor.DataBinding
{
	public class ObservableScriptableObject : ScriptableObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Sets the property if the objects are different (This is in order to prevent recursion with two way binding).
		/// This will return a boolean that states the outcome of the operation.
		/// </summary>
		protected void SetProperty<T>(ref T storage, T value, string propertyName)
		{
			if(Equals(storage, value))
			    return;

		    storage = value;
			UnityEditor.EditorUtility.SetDirty(this);
			OnPropertyChanged(propertyName);
		}

	    private void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler == null)
				return;

			handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}