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

			_mainWindow = LaunchWindow(initializationContext.MainContent);
			_mainWindow.Launch(initializationContext.ScriptableObject);
		}

		public void Launch(ApplicationInitializationContext initializationContext)
		{
			if (initializationContext == null)
				throw new InitializationContextNull();

			_mainWindow = LaunchWindow(initializationContext.MainContent);
			_mainWindow.Launch(initializationContext.ScriptableObject);
		}

		public void Teardown()
		{
			_mainWindow.Close();
		}

		public void RaiseEventFor(string controlId, IEvent raisedEvent)
		{
			_mainWindow.RaiseEventFor(controlId, raisedEvent);
		}

		public void Update()
		{
			_mainWindow.Repaint();
		}

		private static GuacamoleWindow LaunchWindow(Type mainContent)
		{
			var foundWindows = Resources.FindObjectsOfTypeAll(typeof (GuacamoleWindow)) as GuacamoleWindow[];

			if (foundWindows != null)
			{
				foreach (var window in foundWindows)
				{
					if (!window.MatchesMainContent(mainContent))
						continue;
					
					if (!window.AllowMultiple)
						return window;
				}
			}

			var instance = ScriptableObject.CreateInstance<GuacamoleWindow>();
			instance.Show();
			return instance;
		}
	}
}