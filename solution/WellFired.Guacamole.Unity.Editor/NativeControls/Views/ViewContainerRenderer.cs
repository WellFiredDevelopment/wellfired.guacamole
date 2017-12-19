using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(ViewContainer), typeof(ViewContainerRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class ViewContainerRenderer : BaseRenderer
    {
        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
            GUI.Box(UnityRect, "", Style);
        }
    }
}