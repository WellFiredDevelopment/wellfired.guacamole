using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(View), typeof(ViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    public class ViewRenderer : BaseRenderer
    {
        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
            GUI.Box(UnityRect, "", Style);
        }
    }
}