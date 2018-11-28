namespace WellFired.Guacamole.DataStorage.Types
{
	/// <summary>
	/// This interface defines a simple key value store.
	/// </summary>
	public interface IDataStorageService
	{
		/// <summary>
		/// Indicate the location of the storage.
		/// </summary>
		string Location { get; }
		
		/// <summary>
		/// Reads the data that is associated with the given key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		string Read(string key);
		
		/// <summary>
		/// Writes the passed data an associates it with the given key.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		void Write(string data, string key);

		/// <summary>
		/// Delete the data associated to a given key
		/// </summary>
		/// <param name="key"></param>
		void Delete(string key);

		/// <summary>
		/// Returns true if there is data associated to this key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool Exists(string key);	
	}
}