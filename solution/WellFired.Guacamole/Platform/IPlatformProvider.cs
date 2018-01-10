namespace WellFired.Guacamole.Platform
{
	/// <summary>
	/// Provides some platform specific implementations of certain functionalities.
	/// </summary>
	public interface IPlatformProvider
	{
		/// <summary>
		/// Path where data can be saved safely
		/// </summary>
		string FullPlatformDataPath { get; }
		
		/// <summary>
		/// The data path plus the application name
		/// </summary>
		string DataPathWithApplicationName { get; }
		
		/// <summary>
		/// Path where the project assets are saved.
		/// </summary>
		string PlatformProjectPath { get; }

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

		/// <summary>
		/// Allow you to open a folder picker. Since it involves UI, for most platform this function should be called on
		/// the UI thread.
		/// </summary>
		/// <param name="title">Title display on top of the popup window</param>
		/// <param name="folder">The folder that should be opened when the folder opens</param>
		/// <param name="defaultName">The default folder to return if no folder is selected</param>
		/// <returns></returns>
		string OpenFolderPicker(string title, string folder, string defaultName);
	}
}