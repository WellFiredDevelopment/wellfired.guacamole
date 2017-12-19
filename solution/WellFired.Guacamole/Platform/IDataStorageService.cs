namespace WellFired.Guacamole.Platform
{
	/// <summary>
	/// This interface defines a simple key value store.
	/// </summary>
	public interface IDataStorageService
	{
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
	}
}