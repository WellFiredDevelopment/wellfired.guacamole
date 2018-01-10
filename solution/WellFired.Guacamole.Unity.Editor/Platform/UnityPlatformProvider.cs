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
		private const string DataPath = "/GuacamoleApplication/Editor";
		private readonly string _applicationName;

		public UnityPlatformProvider(string applicationName)
		{
			_applicationName = applicationName;
		}

		public string PlatformProjectPath => UnityEngine.Application.dataPath;
		public string FullPlatformDataPath => PlatformProjectPath + DataPath + _applicationName;
		public string DataPathWithApplicationName => DataPath + _applicationName;
		public IMainThreadRunner MainThreadRunner { get; } = new MainThreadRunner();
		public IDataStorageService GetPersonalDataStorage(string applicationName) => new UnityPersonalDataStorageService(_applicationName);
		public IDataStorageService GetTeamSharedDataStorage(string applicationName) => new UnityTeamSharedDataStorageService(_applicationName);
		
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