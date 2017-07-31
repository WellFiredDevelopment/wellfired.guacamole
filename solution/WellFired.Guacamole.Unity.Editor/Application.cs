using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Event;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Unity.Editor.DataBinding;

namespace WellFired.Guacamole.Unity.Editor
{
	[Serializable]
	public class Application : IApplication
	{
		public IWindow MainWindow => _mainWindow;
		[SerializeField] private GuacamoleWindow _mainWindow;
		public bool IsRunning => _mainWindow != null;

		public void Launch<TPersistantData>(ApplicationInitializationContext initializationContext) where TPersistantData : ObservableScriptableObject
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

			_mainWindow = GuacamoleWindowLauncher.LaunchWindow(initializationContext.MainContent);
			_mainWindow.Launch(initializationContext.ScriptableObject);
		}

		public IApplication Launch(ApplicationInitializationContext initializationContext)
		{
			if (initializationContext == null)
				throw new InitializationContextNull();

			_mainWindow = GuacamoleWindowLauncher.LaunchWindow(initializationContext.MainContent);
			_mainWindow.Launch(initializationContext.ScriptableObject);
			return this;
		}

		public void Teardown()
		{
			_mainWindow.CloseAfterNextUpdate = true;
		}

		public void Update()
		{
			_mainWindow.Repaint();
		}
	}
}