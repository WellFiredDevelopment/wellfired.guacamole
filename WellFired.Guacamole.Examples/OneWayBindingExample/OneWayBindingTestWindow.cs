using UnityEditor;
using UnityEngine;

namespace WellFired.Guacamole.Examples.TwoWayBinding
{
	public class OneWayBindingTestWindow : Window
	{
		public OneWayBindingTestWindow()
		{
			Padding = new UIPadding(5);
			
			var boundTextEntry = new TextEntry
			{
				BackgroundColor = UIColor.White
			};

			Content = boundTextEntry;

			const string assetPath = "Assets/guacamole-examples/OneWayBindingExample/Editor/WindowData.asset";
			var windowData = AssetDatabase.LoadAssetAtPath<OneWayBindingTestModel>(assetPath);
			if (windowData == null)
			{
				windowData = ScriptableObject.CreateInstance<OneWayBindingTestModel>();
				AssetDatabase.CreateAsset(windowData, assetPath);
			}

			BindingContext = windowData;

			boundTextEntry.Bind(TextEntry.TextProperty, "BoundText");
		}
	}
}