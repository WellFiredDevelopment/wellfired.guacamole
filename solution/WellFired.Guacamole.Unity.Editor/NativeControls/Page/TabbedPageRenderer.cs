using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Page;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls.Page
{
    public class TabbedPageRenderer : BaseRenderer
    {
        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
            GUI.Box(renderRect.ToUnityRect(), "", Style);
        }
    }
}