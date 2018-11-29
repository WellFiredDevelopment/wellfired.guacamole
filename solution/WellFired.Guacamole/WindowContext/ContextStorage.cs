using WellFired.Guacamole.DataStorage.Data.Serialization;
using WellFired.Guacamole.DataStorage.Types;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.WindowContext
{
	/// <summary>
	/// Context storage store the information of each Guacamole Windows that are closed. This is essential in order to reinitialize the window
	/// that were not closed when Unity restart or compile.
	/// It includes essentially the size of the window, the view type and the view model type. The ids of Guacamole views being unique for each view, the window view id is used as a key
	/// in our storage. We also keep track of all the different window contexts saved in the storage to delete each of them after the
	/// windows were reloaded.
	/// </summary>
	public class ContextStorage
	{
		private const string StoredContextsKey = "Contexts";
		
		private readonly IDataStorageService _storage;
		private readonly ISerializer _serializer;

		public ContextStorage(IDataStorageService storage, ISerializer serializer)
		{
			_storage = storage;
			_serializer = serializer;
			
			CreateStoredContextIfDoesNotExist();
		}

		public Context Load(string windowID)
		{
			return _serializer.Unserialize<Context>(_storage.Read(windowID));
		}

		public void Save(string windowID, Context context)
		{
			var data = _serializer.Serialize(context);
			_storage.Write(data, windowID);
			AddToStoredContexts(windowID);
		}

		public void Delete(string windowID)
		{
			_storage.Delete(windowID);
			RemoveFromStoredContexts(windowID);
		}

		public void CleanUpStoredContexts()
		{
			var storedContexts = _serializer.Unserialize<StoredContexts>(_storage.Read(StoredContextsKey));
			
			foreach (var windowId in storedContexts.ContextIds)
			{
				Delete(windowId);
			}
		}

		private void CreateStoredContextIfDoesNotExist()
		{
			if (!_storage.Exists(StoredContextsKey))
			{
				_storage.Write(_serializer.Serialize(new StoredContexts()), StoredContextsKey);
			}
		}

		private void AddToStoredContexts(string windowID)
		{
			var fileContents = _storage.Read(StoredContextsKey);
			var storedContexts = _serializer.Unserialize<StoredContexts>(_storage.Read(StoredContextsKey)) ?? new StoredContexts();

			storedContexts.ContextIds.Add(windowID);
			_storage.Write(_serializer.Serialize(storedContexts), StoredContextsKey);
		}

		private void RemoveFromStoredContexts(string windowID)
		{
			var storedContexts = _serializer.Unserialize<StoredContexts>(_storage.Read(StoredContextsKey));
			
			storedContexts.ContextIds.Remove(windowID);
			_storage.Write(_serializer.Serialize(storedContexts), StoredContextsKey);
		}
	}
}