using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
	[Serializable]
	public class ObservableScriptableObject : ScriptableObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Sets the property if the objects are different (This is in order to prevent recursion with two way binding).
		///     This will return a boolean that states the outcome of the operation.
		/// </summary>
		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = @"")
		{
			if (Equals(storage, value))
				return false;

			storage = value;
			EditorUtility.SetDirty(this);
			OnPropertyChanged(propertyName);
			return true;
		}

		private void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}