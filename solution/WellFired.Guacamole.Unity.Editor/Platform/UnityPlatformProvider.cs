using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.Unity.Editor.Platform
{
	/// <inheritdoc />
	/// <summary>
	/// An implementation of the Platform Provider for the Unity Engine Platform
	/// </summary>
	public class UnityPlatformProvider : IPlatformProvider
	{
		private readonly string _applicationName;

		public UnityPlatformProvider(string applicationName)
		{
			_applicationName = applicationName;
		}
		
		public IDataStorageService GetPersonalDataStorage(string applicationName) => new UnityPersonalDataStorageService(_applicationName);
		public IDataStorageService GetTeamSharedDataStorage(string applicationName) => new UnityTeamSharedDataStorageService(_applicationName);
	}
}