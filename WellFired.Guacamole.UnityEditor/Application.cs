using UnityEditor;

namespace WellFired.Guacamole.Unity.Editor
{
	public class Application : IApplication
	{
		public void Launch(ApplicationInitializationContext initializationContext)
		{
			if(initializationContext == null)
				throw new InitializationContextNull();
			
			var mainWindow = EditorWindow.GetWindow<GuacamoleWindow>();
			mainWindow.Launch(initializationContext.ScriptableObject);
		}
	}
}