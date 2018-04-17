using WellFired.Guacamole.DataStorage.Storages;

namespace WellFired.Guacamole.Platforms
{
	/// <summary>
	/// Provides some platform specific implementations of certain functionalities.
	/// </summary>
	public interface IPlatformProvider
	{
		/// <summary>
		/// Path where the project is saved.
		/// </summary>
		string ProjectPath { get; }

		/// <summary>
		/// With this, you can get some persistent data storage, you should be able to store strings of data in here.
		/// Think of it as a Key Value Store. This storage is personal because only used by a specific user/machine
		/// </summary>
		IDataStorageService GetPersonalDataStorage();

		/// <summary>
		/// With this, you can get some persistent data storage, you should be able to store strings of data in here.
		/// Think of it as a Key Value Store. This storage is team shared because can be shared with the whole team through
		/// a vcs for example.
		/// </summary>
		IDataStorageService GetTeamSharedDataStorage();

		/// <summary>
		/// Allow you to open a folder picker. Since it involves UI, for most platform this function should be called on
		/// the UI thread.
		/// </summary>
		/// <param name="title">Title display on top of the popup window</param>
		/// <param name="folder">The folder that should be opened when the folder opens</param>
		/// <param name="defaultName">The default folder to return if no folder is selected</param>
		/// <returns></returns>
		string OpenFolderPicker(string title, string folder, string defaultName);

		/// <summary>
		/// Call this method to be returned the full path to a relative team-shared file. If your team-shared data is located at
		/// /path/to/your/shared/data, then calling the function with "Images/Doges.jpeg" will return /path/to/your/shared/data/Images/Doge.jpg.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		string PathToSharedData(string file);
		
		/// <summary>
		/// Call this method to be returned the full path to a relative personal file. If your personal data is located at
		/// /path/to/your/personal/data, then calling the function with "Images/Doges.jpeg" will return /path/to/your/personal/data/Images/Doge.jpg.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		string PathToPersonalData(string file);
		
		/// <summary>
		/// Indicate if the platform application is focused or not.
		/// </summary>
		bool PlatformHasFocus { get; }
	}
}