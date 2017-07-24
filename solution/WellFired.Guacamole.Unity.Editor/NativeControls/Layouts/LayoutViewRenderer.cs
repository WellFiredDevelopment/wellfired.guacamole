using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.NativeControls.Layouts;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(LayoutView), typeof(LayoutViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Layouts
{
	public class LayoutViewRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);
			GUI.Box(UnityRect, "", Style);
		}
	}
}