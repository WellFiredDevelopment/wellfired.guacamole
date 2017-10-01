using System;
using System.IO;
using UnityEditor;
using UnityEngine;
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

		public IApplication Launch(ApplicationInitializationContext initializationContext, Type persistantType)
		{
			if (initializationContext == null)
				throw new InitializationContextNull();

			var assetPath = Device.AdjustPath($"{initializationContext.ApplicationName}/{initializationContext.ApplicationName}.asset");
			var persistantData = AssetDatabase.LoadAssetAtPath(assetPath, persistantType);
			
			if (persistantData == null)
			{
				persistantData = ScriptableObject.CreateInstance(persistantType);
				Directory.CreateDirectory(Device.AdjustPath(initializationContext.ApplicationName));
				AssetDatabase.DeleteAsset(assetPath);
				AssetDatabase.CreateAsset(persistantData, assetPath);
				EditorUtility.SetDirty(persistantData);
				AssetDatabase.SaveAssets();
			}

			initializationContext.PersistantData = (ObservableScriptableObject) persistantData;

			_mainWindow = GuacamoleWindowLauncher.LaunchWindow(initializationContext.MainContent);
			_mainWindow.Launch(initializationContext.ScriptableObject);
			
			return this;
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