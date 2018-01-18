namespace WellFired.Guacamole.DataStorage.Storages
{
	/// <summary>
	/// Save data in key/value fashion inside a folder specified this way :
	/// [project path (unity project for example)]/.[company name]/.[application name]/.personalData/.keys.
	/// Therefore, it is specific to the application and the project it is running in.
	/// Note that this data should not be commited to any VCS and that it should not influence the reproductibility of a build.
	/// </summary>
	public class PersonalDataStorage : FileStorageService
	{
		public static string Location(string projectPath, string applicationName, string companyName) => 
			$"{projectPath}/.{companyName.ToLower()}/.{applicationName}/.personalData";
		
		public PersonalDataStorage(string applicationName, string companyName, string path) :
			base($"{Location(path, applicationName, companyName)}/.keys")
		{
		}
	}
}