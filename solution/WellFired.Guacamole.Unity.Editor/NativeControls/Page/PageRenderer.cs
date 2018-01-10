using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Pages;
using WellFired.Guacamole.Unity.Editor.NativeControls.Page;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;

[assembly: CustomRenderer(typeof(Page), typeof(PageRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls.Page
{
    public class PageRenderer : BaseRenderer
    {
        public override void Render(UIRect renderRect)
        {
            base.Render(renderRect);
            GUI.Box(UnityRect, "", Style);
        }
    }
}