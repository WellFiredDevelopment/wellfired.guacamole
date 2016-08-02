using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(Slider), typeof(WellFired.Guacamole.Unity.Editor.SliderRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class SliderRenderer : BaseRenderer
	{
		public override void Render(UIRect renderRect)
		{
			var slider = Control as Slider;
			slider.Value = UnityEditor.EditorGUI.Slider(renderRect.ToUnityRect(), (float)slider.Value, (float)slider.MinValue, (float)slider.MaxValue);
		}
	}
}