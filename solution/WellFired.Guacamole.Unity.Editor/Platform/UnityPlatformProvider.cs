using UnityEditor;
using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.Unity.Editor.Platform
{
	/// <inheritdoc />
	/// <summary>
	/// An implementation of the Platform Provider for the Unity Engine Platform
	/// </summary>
	public class UnityPlatformProvider : IPlatformProvider
	{
		private const string DataPath = "/GuacamoleApplication/Editor/";
		private readonly string _applicationName;

		public UnityPlatformProvider(string applicationName)
		{
			_applicationName = applicationName;
		}

		public string PlatformProjectPath => UnityEngine.Application.dataPath;
		public string ApplicationDataRootedPath => PlatformProjectPath + DataPath + _applicationName;
		public string ApplicationDataRelativePath => DataPath + _applicationName;
		public IDataStorageService GetPersonalDataStorage() => new UnityPersonalDataStorageService(_applicationName);
		public IDataStorageService GetTeamSharedDataStorage() => new UnityTeamSharedDataStorageService(_applicationName);
		
		public string OpenFolderPicker(string title, string folder, string defaultName)
		{
			return EditorUtility.OpenFolderPanel(title, folder, defaultName);
		}

		public string PathTo(string file)
		{
			return $"Assets{DataPath}/{_applicationName}/{file}";
		}
	}
}