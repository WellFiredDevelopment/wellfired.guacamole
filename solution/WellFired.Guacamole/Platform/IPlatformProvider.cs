namespace WellFired.Guacamole.Platform
{
	/// <summary>
	/// Provides some platform specific implementations of certain functionalities.
	/// </summary>
	public interface IPlatformProvider
	{
		/// <summary>
		/// With this, you can get some persistent data storage, you should be able to store strings of data in here.
		/// Think of it as a Key Value Store.
		/// </summary>
		IDataStorageService GetPersonalDataStorage(string applicationName);
		
		/// <summary>
		/// With this, you can get some persistent data storage, you should be able to store strings of data in here.
		/// Think of it as a Key Value Store.
		/// </summary>
		IDataStorageService GetTeamSharedDataStorage(string applicationName);
	}
}