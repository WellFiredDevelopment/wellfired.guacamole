using WellFired.Guacamole.Platform;
using WellFired.Guacamole.StoredData.Serialization;

namespace WellFired.Guacamole.WindowContext
{
	public class ContextStorage
	{
		private const string StoredContextsKey = "StoredContexts";
		
		public static bool WasCleanedUp { get; private set; }
		
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
			_storage.Write(_serializer.Serialize(context), windowID);
			AddToStoredContexts(windowID);
		}

		public void Delete(string windowID)
		{
			_storage.Delete(windowID);
			RemoveFromStoredContexts(windowID);
		}

		public void CleanUpStoredContexts()
		{
			WasCleanedUp = true;
			
			var storedContexts = _serializer.Unserialize<StoredContexts>(_storage.Read(StoredContextsKey));
			
			foreach (var windowId in storedContexts.ContextIds)
			{
				Delete(windowId);
			}
		}

		private void CreateStoredContextIfDoesNotExist()
		{
			var storedContext = _serializer.Unserialize<StoredContexts>(_storage.Read(StoredContextsKey));
			if (storedContext?.ContextIds == null)
			{
				_storage.Write(_serializer.Serialize(new StoredContexts()), StoredContextsKey);
			}
		}

		private void AddToStoredContexts(string windowID)
		{
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