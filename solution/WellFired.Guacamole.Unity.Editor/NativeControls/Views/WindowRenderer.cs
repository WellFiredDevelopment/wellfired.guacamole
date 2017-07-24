using UnityEditor;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(Window), typeof(WindowRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class WindowRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);
			EditorGUI.DrawRect(UnityRect, Control.BackgroundColor.ToUnityColor());
		}
	}
}