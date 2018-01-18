using System.IO;
using UnityEditor;
using WellFired.Guacamole.DataStorage.Storages;
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
		private readonly string _companyName;

		public UnityPlatformProvider(string applicationName, string companyName)
		{
			_applicationName = applicationName;
			_companyName = companyName;
		}

		public string AssetsPath => UnityEngine.Application.dataPath;
		public string ProjectPath => Path.GetFullPath($"{AssetsPath}/..");
		public IDataStorageService GetPersonalDataStorage() => new PersonalDataStorage(_applicationName, _companyName, ProjectPath);
		public IDataStorageService GetTeamSharedDataStorage() => new TeamSharedDataStorage(_applicationName, _companyName, ProjectPath);
		
		public string OpenFolderPicker(string title, string folder, string defaultName)
		{
			return EditorUtility.OpenFolderPanel(title, folder, defaultName);
		}

		public string PathToSharedData(string file)
		{
			return $"{TeamSharedDataStorage.Location(ProjectPath, _applicationName, _companyName)}/{file}";
		}

		public string PathToPersonalData(string file)
		{
			return $"{PersonalDataStorage.Location(ProjectPath, _applicationName, _companyName)}/{file}";
		}
	}
}