using System.IO;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Unity.Editor.DataBinding;

namespace WellFired.Guacamole.Unity.Editor
{
	public class Application : IApplication
	{
		public void Launch<TPersistantData>(ApplicationInitializationContext initializationContext) where TPersistantData : ObservableScriptableObject
        {
			if(initializationContext == null)
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

            var mainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
            mainWindow.Launch(initializationContext.ScriptableObject);
        }

        public void Launch(ApplicationInitializationContext initializationContext)
        {
            if (initializationContext == null)
                throw new InitializationContextNull();

            var mainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
            mainWindow.Launch(initializationContext.ScriptableObject);
        }
    }
}