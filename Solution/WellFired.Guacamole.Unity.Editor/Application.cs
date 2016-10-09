using System.IO;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Unity.Editor.DataBinding;

namespace WellFired.Guacamole.Unity.Editor
{
	public class Application : IApplication
	{
		private GuacamoleWindow _mainWindow;
		public bool IsRunning => _mainWindow != null;

		public void Launch<TPersistantData>(ApplicationInitializationContext initializationContext)
			where TPersistantData : ObservableScriptableObject
		{
			if (initializationContext == null)
				throw new InitializationContextNull();

			const string assetPath = "Assets/Editor/PersistantData.asset";
			var persistantData = AssetDatabase.LoadAssetAtPath<TPersistantData>(assetPath);
			if (persistantData == null)
			{
				persistantData = ScriptableObject.CreateInstance<TPersistantData>();
				Directory.CreateDirectory("Assets/Editor");

				AssetDatabase.DeleteAsset(assetPath);
				AssetDatabase.CreateAsset(persistantData, assetPath);
				EditorUtility.SetDirty(persistantData);
				AssetDatabase.SaveAssets();
			}

			initializationContext.PersistantData = persistantData;

			_mainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
			_mainWindow.Launch(initializationContext.ScriptableObject);
		}

		public void Launch(ApplicationInitializationContext initializationContext)
		{
			if (initializationContext == null)
				throw new InitializationContextNull();

			_mainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
			_mainWindow.Launch(initializationContext.ScriptableObject);
		}

		public void Teardown()
		{
			_mainWindow.Close();
		}
	}
}