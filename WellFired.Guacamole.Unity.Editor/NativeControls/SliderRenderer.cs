using System.Diagnostics;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls;
using WellFired.Guacamole.View;

[assembly : CustomRenderer(typeof(Slider), typeof(SliderRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public class SliderRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			var slider = Control as Slider;

            Debug.Assert(slider != null, "slider != null");

		    slider.Value = UnityEditor.EditorGUI.Slider(renderRect.ToUnityRect(), (float)slider.Value, (float)slider.MinValue, (float)slider.MaxValue);
		}
	}
}