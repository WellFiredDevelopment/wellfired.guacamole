using System.IO;
using System.Linq;
using UnityEditor;
using WellFired.Guacamole.DataStorage.Storages;
using WellFired.Guacamole.Platforms;

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

		public string ProjectPath => Path.GetFullPath($"{UnityEngine.Application.dataPath}/..");
		public string AssetPath => $"{ProjectPath}/Assets";
		
		public IDataStorageService GetPersonalDataStorage() => new FileStorageService(PathToPersonalData(".keys"));
		public IDataStorageService GetTeamSharedDataStorage() => new FileStorageService(PathToSharedData("Keys"));
		
		public string OpenFolderPicker(string title, string folder, string defaultName)
		{
			return EditorUtility.OpenFolderPanel(title, folder, defaultName);
		}

		public string PathToSharedData(string file)
		{
			return $"{ProjectPath}/{_companyName}/{_applicationName}/Teamshared/{file}";
		}

		public string PathToPersonalData(string file)
		{
			return $"{ProjectPath}/.{_companyName.ToLower()}/.{_applicationName}/.personalData/{file}";
		}

		public string[] FindAssets(string search)
		{
			AssetDatabase.FindAssets(search);

			var assetsPath = AssetDatabase.FindAssets(search).Select(AssetDatabase.GUIDToAssetPath)
				.Select(path => ProjectPath + "/" + path);

			return assetsPath.ToArray();
		}

		public bool PlatformHasFocus => EditorWindow.focusedWindow != null;
	}
}