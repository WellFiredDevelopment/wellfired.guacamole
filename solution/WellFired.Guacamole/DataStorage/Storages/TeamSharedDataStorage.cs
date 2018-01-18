namespace WellFired.Guacamole.DataStorage.Storages
{
	/// <summary>
	/// Save data in key/value fashion inside a folder specified this way :
	/// [project path (unity project for example)]/[company name]/[application name]/Teamshared/Keys.
	/// Therefore, it is specific to the application and the project it is running in.
	///Note that this data is supposed to be shared with the team, so commited to a VCS.
	/// </summary>
	public class TeamSharedDataStorage : FileStorageService
	{
		public static string Location(string projectPath, string applicationName, string companyName) => 
			$"{projectPath}/{companyName}/{applicationName}/Teamshared";

		public TeamSharedDataStorage(string applicationName, string companyName, string path) :
			base($"{Location(path, applicationName, companyName)}/Keys")
		{
		}
	}
}