using System;
using System.Runtime.CompilerServices;
using WellFired.Guacamole.DataStorage.Data.Serialization;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.DataStorage.Data.Synchronization
{
	/// <summary>
	/// Any implementation of this class will have the possibility to have its properties to be synchronized with 
	/// the stored data of the generic type through the <see cref="DataAccess"/>. The stored data format must be JSON.
	/// <seealso cref="DataAccess"/><seealso cref="IDataProxy"/>
	/// </summary>
	/// <remarks>The stored data should be a class composed of fields serialized under JSON format. The properties of the <see cref="DataProxy{T}"/>
	/// should has the same name as the stored data fields, and call <see cref="SetProperty{TY}"/> to notify it changed. Here an example :
	/// <code>
	///	private class OptionsProxy : DataProxy{Options}
	///	{
	///		private bool _openAutomatically;
	///		private string _relativePath; 
	///
	///		public bool OpenAutomatically
	///		{
	///			get => _openAutomatically;
	///			set => SetProperty(ref _openAutomatically, value);
	///		}
	///
	///		public string RelativePath
	///		{
	///			get => _relativePath;
	///			set => SetProperty(ref _relativePath, value);
	///		}
	///	}
	/// </code>
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	public abstract class DataProxy<T> : IDataProxy where T : class, new()
	{
		private readonly JSONSerializer _serializer = new JSONSerializer();
		private FieldReflector<T> _fieldReflector;
		private T _cachedData;

		private bool _injecting;

		/// <inheritdoc />
		public bool DataChanged { get; protected set; }

		/// <inheritdoc />
		public virtual void InjectData(string data)
		{
			try
			{
				_cachedData = null;
				
				if (!string.IsNullOrEmpty(data))
				{
					_cachedData = _serializer.Unserialize<T>(data);
				}
				
				if (_cachedData == null)
				{
					_cachedData = Activator.CreateInstance<T>();
					DataChanged = true;
				}
			}
			catch (Exception e)
			{
				Logger.LogError($"Could not parse injected data : {e.Message}\n{e.StackTrace}. New data object will be instantiated.");
				_cachedData = Activator.CreateInstance<T>();
				DataChanged = true;
			}
			
			_fieldReflector = new FieldReflector<T>(_cachedData, this);
			_injecting = true;
			_fieldReflector.ReflectFieldsToProperties();
			_injecting = false;
		}

		/// <inheritdoc />
		public string GetData()
		{
			return _serializer.Serialize(_cachedData);
		}

		/// <inheritdoc />
		public void ResetDataChanged()
		{
			DataChanged = false;
		}

		protected void SetProperty<TY>(ref TY storage, TY value, [CallerMemberName] string propertyName = @"")
		{
			if (_cachedData == null)
			{
				InitializeWithDefaultData();
			}
			
			if (Equals(storage, value))
				return;
		
			storage = value;
		
			if (_injecting)
			{
				return;
			}
				
			_fieldReflector.ReflectPropertyToFields(propertyName, value);
			DataChanged = true;
		}

		private void InitializeWithDefaultData()
		{
			InjectData(null);
		}
	}
}